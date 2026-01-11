@echo off
chcp 65001 > nul
setlocal EnableDelayedExpansion

:: ========================================
:: 配置区域 - 根据你的项目修改
:: ========================================

:: ⭐ 项目路径配置（相对或绝对路径）
:: 示例1：相对路径
set PROJECT_DIR=.

:: ⭐ .csproj 文件名（不含路径）
set CSPROJ_FILE=TwinCatTool.csproj


echo.
echo ========================================
echo   跨平台打包脚本（自动读取配置）
echo ========================================
echo.

:: 检查项目路径是否存在
if not exist "%PROJECT_DIR%" (
    echo [错误] 项目路径不存在: %PROJECT_DIR%
    pause
    exit /b 1
)

:: 进入项目目录
cd /d "%PROJECT_DIR%"
if errorlevel 1 (
    echo [错误] 无法进入项目目录
    pause
    exit /b 1
)

echo [信息] 当前工作目录: %CD%
echo.

:: 检查 .csproj 文件是否存在
if not exist "%CSPROJ_FILE%" (
    echo [错误] 找不到 %CSPROJ_FILE% 文件
    echo [提示] 请确认以下内容：
    echo   1. PROJECT_DIR 路径是否正确
    echo   2. CSPROJ_FILE 文件名是否正确
    echo.
    echo 当前目录中的 .csproj 文件：
    dir /b *.csproj 2>nul
    pause
    exit /b 1
)

echo [信息] 找到项目文件: %CSPROJ_FILE%
echo.

:: ========================================
:: 从 .csproj 文件中读取配置
:: ========================================
echo [0/3] 读取项目配置...
echo.

:: 创建 PowerShell 脚本文件
echo $xml = [xml](Get-Content '%CSPROJ_FILE%') > temp_read.ps1
echo $pg = $xml.Project.PropertyGroup >> temp_read.ps1
echo if ($pg -is [array]) { $pg = $pg[0] } >> temp_read.ps1
echo $assemblyName = if ($pg.AssemblyName) { $pg.AssemblyName } else { '%CSPROJ_FILE%'.Replace('.csproj', '') } >> temp_read.ps1
echo $version = if ($pg.Version) { $pg.Version } elseif ($pg.AssemblyVersion) { $pg.AssemblyVersion } else { '1.0.0' } >> temp_read.ps1
echo $title = if ($pg.Title) { $pg.Title } elseif ($pg.Product) { $pg.Product } else { $assemblyName } >> temp_read.ps1
echo $authors = if ($pg.Authors) { $pg.Authors } elseif ($pg.Company) { $pg.Company } else { 'Author' } >> temp_read.ps1
echo Write-Output "SET APP_NAME=$assemblyName" >> temp_read.ps1
echo Write-Output "SET APP_VERSION=$version" >> temp_read.ps1
echo Write-Output "SET APP_TITLE=$title" >> temp_read.ps1
echo Write-Output "SET APP_AUTHORS=$authors" >> temp_read.ps1

:: 执行 PowerShell 脚本
powershell -ExecutionPolicy Bypass -File temp_read.ps1 > temp_config.bat

if errorlevel 1 (
    echo [错误] 读取 .csproj 文件失败
    pause
    exit /b 1
)

:: 执行生成的配置
call temp_config.bat
del temp_config.bat
del temp_read.ps1

:: 验证读取结果
if "%APP_NAME%"=="" (
    echo [错误] 无法读取 AssemblyName，使用默认值
    for %%F in ("%CSPROJ_FILE%") do set APP_NAME=%%~nF
)

if "%APP_VERSION%"=="" (
    echo [警告] 无法读取 Version，使用默认值
    set APP_VERSION=1.0.0
)

if "%APP_TITLE%"=="" (
    set APP_TITLE=%APP_NAME%
)

if "%APP_AUTHORS%"=="" (
    set APP_AUTHORS=Author
)

echo [配置信息]
echo   应用名称 (AssemblyName): %APP_NAME%
echo   版本号 (Version): %APP_VERSION%
echo   标题 (Title): %APP_TITLE%
echo   作者 (Authors): %APP_AUTHORS%
echo   输出目录: %OUTPUT_DIR%
echo.
echo ✓ 配置读取完成
echo.

:: ========================================
:: 步骤1：清理旧文件
:: ========================================
echo [1/3] 清理旧文件...
if exist "bin\Release" rd /s /q "bin\Release"
if exist "obj\Release" rd /s /q "obj\Release"
echo ✓ 清理完成
echo.

:: ========================================
:: 步骤2：恢复依赖
:: ========================================
echo [2/3] 恢复 NuGet 依赖...
dotnet restore
if errorlevel 1 (
    echo ✗ 依赖恢复失败
    pause
    exit /b 1
)
echo ✓ 依赖恢复完成
echo.

:: ========================================
:: 步骤3：编译和发布所有平台
:: ========================================
echo [3/3] 编译和发布所有平台...
echo.

:: Windows x64
echo   - 发布 Windows x64...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false
if errorlevel 1 (
    echo   ✗ Windows x64 发布失败
    pause
    exit /b 1
)
echo   ✓ Windows x64 发布完成
echo.

:: Windows x86
echo   - 发布 Windows x86...
dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=false
if errorlevel 1 (
    echo   ✗ Windows x86 发布失败
    pause
    exit /b 1
)
echo   ✓ Windows x86 发布完成
echo.

echo ✓ 所有平台发布完成
echo.

pause