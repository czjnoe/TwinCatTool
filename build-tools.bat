@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion

:: ==================== 配置区域 ====================
:: 项目配置
set PROJECT_NAME=TwinCatTool
set SOLUTION_FILE=%PROJECT_NAME%.sln
set PROJECT_FILE=%PROJECT_NAME%.csproj

:: 构建配置
set BUILD_CONFIG=Release

:: 输出目录
set OUTPUT_BASE_DIR=.\bin\Release\publish
set BACKUP_DIR=.\backup

:: ==================== 颜色定义 ====================
set "ESC="
set "GREEN=%ESC%[92m"
set "RED=%ESC%[91m"
set "YELLOW=%ESC%[93m"
set "BLUE=%ESC%[94m"
set "NC=%ESC%[0m"

:: ==================== 主菜单 ====================
:MENU
cls
echo.
echo ============================================
echo      .NET 项目构建发布工具 v2.0
echo ============================================
echo.
echo 当前项目: %PROJECT_NAME%
echo 构建配置: %BUILD_CONFIG%
echo.
echo ============================================
echo.
echo [1] 清理项目 (Clean)
echo [2] 还原依赖 (Restore)
echo [3] 构建项目 (Build)
echo [4] 运行项目 (Run)
echo [5] 发布项目 (Publish)
echo [6] 批量发布 (Multi-Runtime Publish)
echo [7] 完整流程 (Clean + Restore + Build + Publish)
echo [8] 运行测试 (Test)
echo [9] 打包 NuGet (Pack)
echo [B] 备份发布文件
echo [S] 修改设置
echo [0] 退出
echo.
echo ============================================
echo.
set /p choice="请选择操作 [0-9/B/S]: "

if /i "%choice%"=="1" goto CLEAN
if /i "%choice%"=="2" goto RESTORE
if /i "%choice%"=="3" goto BUILD
if /i "%choice%"=="4" goto RUN
if /i "%choice%"=="5" goto PUBLISH
if /i "%choice%"=="6" goto MULTI_PUBLISH
if /i "%choice%"=="7" goto FULL_PROCESS
if /i "%choice%"=="8" goto TEST
if /i "%choice%"=="9" goto PACK
if /i "%choice%"=="B" goto BACKUP
if /i "%choice%"=="b" goto BACKUP
if /i "%choice%"=="S" goto SETTINGS
if /i "%choice%"=="s" goto SETTINGS
if "%choice%"=="0" goto END
goto MENU

:: ==================== 清理项目 ====================
:CLEAN
echo.
echo [INFO] 开始清理项目...
dotnet clean %SOLUTION_FILE% --configuration %BUILD_CONFIG%
if %errorlevel% neq 0 (
    echo [ERROR] 清理失败！
    pause
    goto MENU
)
echo [SUCCESS] 清理完成！
pause
goto MENU

:: ==================== 还原依赖 ====================
:RESTORE
echo.
echo [INFO] 开始还原 NuGet 包...
dotnet restore %SOLUTION_FILE%
if %errorlevel% neq 0 (
    echo [ERROR] 还原失败！
    pause
    goto MENU
)
echo [SUCCESS] 还原完成！
pause
goto MENU

:: ==================== 构建项目 ====================
:BUILD
echo.
echo [INFO] 开始构建项目...
dotnet build %SOLUTION_FILE% --configuration %BUILD_CONFIG% --no-restore
if %errorlevel% neq 0 (
    echo [ERROR] 构建失败！
    pause
    goto MENU
)
echo [SUCCESS] 构建完成！
pause
goto MENU

:: ==================== 运行项目 ====================
:RUN
echo.
echo [INFO] 开始运行项目...
dotnet run --project %PROJECT_FILE% --configuration %BUILD_CONFIG%
pause
goto MENU

:: ==================== 获取项目类型和目标框架 ====================
:GET_PROJECT_INFO
cls
echo.
echo ============================================
echo           选择项目类型
echo ============================================
echo.
echo [1] Windows 桌面应用 (WPF/WinForms)
echo [2] 控制台应用 (Console)
echo [3] 类库 (Class Library)
echo [4] Web API / ASP.NET Core
echo [5] Blazor WebAssembly
echo [6] Blazor Server
echo [7] 其他 (手动输入)
echo.
set /p proj_type="请选择项目类型 [1-7]: "

:: 根据项目类型设置推荐的目标框架
if "%proj_type%"=="1" (
    set "RECOMMENDED_FRAMEWORK=net8.0-windows"
    set "PROJECT_TYPE_NAME=Windows 桌面应用"
)
if "%proj_type%"=="2" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=控制台应用"
)
if "%proj_type%"=="3" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=类库"
)
if "%proj_type%"=="4" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=Web API"
)
if "%proj_type%"=="5" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=Blazor WebAssembly"
)
if "%proj_type%"=="6" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=Blazor Server"
)
if "%proj_type%"=="7" (
    set "RECOMMENDED_FRAMEWORK=net8.0"
    set "PROJECT_TYPE_NAME=自定义项目"
)

echo.
echo 项目类型: !PROJECT_TYPE_NAME!
echo 推荐框架: !RECOMMENDED_FRAMEWORK!
echo.
echo 常用目标框架:
echo   - net9.0-windows  (Windows 桌面)
echo   - net8.0          (最新 LTS)
echo   - net8.0-windows  (Windows 桌面)
echo   - net7.0-windows  (Windows 桌面)
echo   - net6.0-windows  (Windows 桌面)
echo.
set /p TARGET_FRAMEWORK="请输入目标框架 (直接回车使用推荐: !RECOMMENDED_FRAMEWORK!): "
if "!TARGET_FRAMEWORK!"=="" set "TARGET_FRAMEWORK=!RECOMMENDED_FRAMEWORK!"

echo.
echo [INFO] 目标框架设置为: !TARGET_FRAMEWORK!
goto :eof

:: ==================== 获取运行时配置 ====================
:GET_RUNTIME_INFO
echo.
echo ============================================
echo           选择运行时平台
echo ============================================
echo.
echo [1] Windows x64 (推荐)
echo [2] Windows x86 (32位)
echo [3] Windows ARM64
echo [4] Linux x64
echo [5] Linux ARM64
echo [6] macOS x64 (Intel)
echo [7] macOS ARM64 (Apple Silicon)
echo [8] 多运行时 (稍后选择多个)
echo [9] 框架依赖 (不指定运行时)
echo [0] 手动输入
echo.
set /p runtime_choice="请选择运行时 [0-9]: "

if "%runtime_choice%"=="1" set "RUNTIME=win-x64"
if "%runtime_choice%"=="2" set "RUNTIME=win-x86"
if "%runtime_choice%"=="3" set "RUNTIME=win-arm64"
if "%runtime_choice%"=="4" set "RUNTIME=linux-x64"
if "%runtime_choice%"=="5" set "RUNTIME=linux-arm64"
if "%runtime_choice%"=="6" set "RUNTIME=osx-x64"
if "%runtime_choice%"=="7" set "RUNTIME=osx-arm64"
if "%runtime_choice%"=="8" (
    set "RUNTIME=multi"
    goto :eof
)
if "%runtime_choice%"=="9" (
    set "RUNTIME="
    echo [INFO] 框架依赖模式，不指定运行时
    goto :eof
)
if "%runtime_choice%"=="0" (
    echo.
    echo 更多 RID: https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog
    set /p RUNTIME="请输入运行时标识符 (RID): "
    goto :eof
)

echo [INFO] 运行时设置为: !RUNTIME!
goto :eof

:: ==================== 获取发布配置 ====================
:GET_PUBLISH_OPTIONS
echo.
echo ============================================
echo           发布配置选项
echo ============================================
echo.

:: 自包含部署
if not "!RUNTIME!"=="" (
    echo [1] 部署模式
    echo    - 自包含: 包含 .NET 运行时，体积大但无需安装 .NET
    echo    - 框架依赖: 体积小但需要目标机器安装 .NET
    echo.
    set /p sc_choice="是否使用自包含部署? (Y/N，默认N): "
    if /i "!sc_choice!"=="Y" (
        set SELF_CONTAINED=true
        echo [选择] 自包含部署
    ) else (
        set SELF_CONTAINED=false
        echo [选择] 框架依赖部署
    )
) else (
    set SELF_CONTAINED=false
    echo [INFO] 框架依赖模式（未指定运行时）
)

echo.
:: 单文件发布
if "!SELF_CONTAINED!"=="true" (
    echo [2] 单文件发布
	echo    - 将所有应用程序文件打包成单个可执行文件
	echo    - 优点: 便于分发    缺点: 启动稍慢
	echo.
    set /p sf_choice="是否生成单文件? (Y/N，默认N): "
    if /i "!sf_choice!"=="Y" (
        set SINGLE_FILE=true
        echo [选择] 单文件模式
    ) else (
        set SINGLE_FILE=false
        echo [选择] 多文件模式
    )
) else (
    set SINGLE_FILE=false
)

echo.
:: ReadyToRun
echo [3] ReadyToRun 编译 (提升启动速度)
echo    - 预编译程序集，提升启动速度
echo    - 优点: 启动快    缺点: 文件体积增大
echo.
set /p r2r_choice="是否启用 ReadyToRun? (Y/N，默认N): "
if /i "!r2r_choice!"=="Y" (
    set READY_TO_RUN=true
    echo [选择] 启用 ReadyToRun
) else (
    set READY_TO_RUN=false
    echo [选择] 不使用 ReadyToRun
)

echo.
:: 裁剪未使用代码
if "!SELF_CONTAINED!"=="true" (
    echo [4] 裁剪未使用代码 (减小体积，可能影响反射)
	echo    - 移除未使用的程序集和代码，减小体积
	echo    - 注意: 可能影响反射等动态功能
	echo.
    set /p trim_choice="是否裁剪未使用代码? (Y/N，默认N): "
    if /i "!trim_choice!"=="Y" (
        set TRIM_UNUSED=true
        echo [选择] 启用代码裁剪
    ) else (
        set TRIM_UNUSED=false
        echo [选择] 不裁剪代码
    )
) else (
    set TRIM_UNUSED=false
)

echo.
:: 启用压缩
if "!SINGLE_FILE!"=="true" (
    echo [5] 程序集压缩
	echo    - 压缩程序集以减小体积
	echo    - 优点: 体积小    缺点: 运行时需解压，稍影响性能
	echo.
    set /p compress_choice="是否启用程序集压缩? (Y/N，默认N): "
    if /i "!compress_choice!"=="Y" (
        set ENABLE_COMPRESSION=true
        echo [选择] 启用压缩
    ) else (
        set ENABLE_COMPRESSION=false
        echo [选择] 不压缩
    )
) else (
    set ENABLE_COMPRESSION=false
)

echo.
:: 包含本地库
if "!SINGLE_FILE!"=="true" (
    echo [6] 包含本地库到单文件
	echo    - 将 native 库也打包到单文件中
    echo    - 进一步减少文件数量
    echo.
    set /p native_choice="是否包含本地库? (Y/N，默认N): "
    if /i "!native_choice!"=="Y" (
        set INCLUDE_NATIVE_LIBS=true
        echo [选择] 包含本地库
    ) else (
        set INCLUDE_NATIVE_LIBS=false
        echo [选择] 不包含本地库
    )
) else (
    set INCLUDE_NATIVE_LIBS=false
)

echo.
:: 分层编译
echo [7] 分层编译 (运行时性能优化)
echo    - 启用 JIT 编译器的分层编译优化
echo    - 优点: 运行时性能更好    缺点: 启动稍慢
echo.
set /p tier_choice="是否启用分层编译? (Y/N，默认Y): "
if /i "!tier_choice!"=="N" (
    set TIERED_COMPILATION=false
    echo [选择] 禁用分层编译
) else (
    set TIERED_COMPILATION=true
    echo [选择] 启用分层编译
)

echo.
:: 调试信息
echo [8] 调试信息
echo    - None: 无调试信息
echo    - Embedded: 内嵌到程序集
echo    - Portable: 生成 .pdb 文件
echo.
set /p debug_choice="选择调试信息类型 (N/E/P，默认N): "
if /i "!debug_choice!"=="E" (
    set DEBUG_TYPE=embedded
    echo [选择] 内嵌调试信息
) else if /i "!debug_choice!"=="P" (
    set DEBUG_TYPE=portable
    echo [选择] 生成 PDB 文件
) else (
    set DEBUG_TYPE=none
    echo [选择] 无调试信息
)

goto :eof

:: ==================== 执行发布 ====================
:EXECUTE_PUBLISH
echo.
echo ============================================
echo [INFO] 开始发布项目...
echo ============================================

:: 设置输出目录
if "!RUNTIME!"=="" (
    set "OUTPUT_DIR=!OUTPUT_BASE_DIR!\!TARGET_FRAMEWORK!"
) else (
    set "OUTPUT_DIR=!OUTPUT_BASE_DIR!\!TARGET_FRAMEWORK!\!RUNTIME!"
)

:: 创建输出目录
if not exist "!OUTPUT_DIR!" mkdir "!OUTPUT_DIR!"

:: 构建发布命令
set PUBLISH_CMD=dotnet publish %PROJECT_FILE% ^
    --configuration %BUILD_CONFIG% ^
    --framework !TARGET_FRAMEWORK! ^
    --output "!OUTPUT_DIR!"

:: 添加运行时
if not "!RUNTIME!"=="" (
    set PUBLISH_CMD=!PUBLISH_CMD! --runtime !RUNTIME!
)

:: 添加自包含
if not "!RUNTIME!"=="" (
    set PUBLISH_CMD=!PUBLISH_CMD! --self-contained !SELF_CONTAINED!
)

:: 添加单文件
if "!SINGLE_FILE!"=="true" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:PublishSingleFile=true
)

:: 添加 ReadyToRun
if "!READY_TO_RUN!"=="true" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:PublishReadyToRun=true
)

:: 添加裁剪
if "!TRIM_UNUSED!"=="true" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:PublishTrimmed=true
)

:: 添加压缩
if "!ENABLE_COMPRESSION!"=="true" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:EnableCompressionInSingleFile=true
)

:: 添加本地库
if "!INCLUDE_NATIVE_LIBS!"=="true" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:IncludeNativeLibrariesForSelfExtract=true
)

:: 添加分层编译
if "!TIERED_COMPILATION!"=="false" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:TieredCompilation=false
)

:: 添加调试信息
if not "!DEBUG_TYPE!"=="" (
    set PUBLISH_CMD=!PUBLISH_CMD! -p:DebugType=!DEBUG_TYPE!
)

:: 执行发布
echo.
echo [CMD] !PUBLISH_CMD!
echo.
!PUBLISH_CMD!

if %errorlevel% neq 0 (
    echo.
    echo [ERROR] 发布失败！
    goto :eof
)

echo.
echo [SUCCESS] 发布完成！
echo [INFO] 输出目录: !OUTPUT_DIR!
echo.
echo 发布配置总结:
echo - 目标框架: !TARGET_FRAMEWORK!
if not "!RUNTIME!"=="" echo - 运行时: !RUNTIME!
if not "!RUNTIME!"=="" echo - 部署模式: !SELF_CONTAINED!
echo - 单文件: !SINGLE_FILE!
echo - ReadyToRun: !READY_TO_RUN!
echo - 代码裁剪: !TRIM_UNUSED!
echo - 压缩: !ENABLE_COMPRESSION!
if "!SINGLE_FILE!"=="true" echo - 包含本地库: !INCLUDE_NATIVE_LIBS!
echo - 分层编译: !TIERED_COMPILATION!
echo - 调试信息: !DEBUG_TYPE!
echo.

goto :eof

:: ==================== 发布项目 ====================
:PUBLISH
call :GET_PROJECT_INFO
call :GET_RUNTIME_INFO
call :GET_PUBLISH_OPTIONS
call :EXECUTE_PUBLISH
pause
goto MENU

:: ==================== 批量发布多个运行时 ====================
:MULTI_PUBLISH
call :GET_PROJECT_INFO

echo.
echo ============================================
echo         批量发布 - 选择运行时
echo ============================================
echo.
echo 请选择要发布的运行时 (可多选，用空格分隔)
echo.
echo [1] Windows x64
echo [2] Windows x86
echo [3] Windows ARM64
echo [4] Linux x64
echo [5] Linux ARM64
echo [6] macOS x64
echo [7] macOS ARM64
echo.
echo 示例: 输入 "1 2" 将同时发布 Windows x64 和 x86
echo.
set /p runtime_selections="请输入选项 (如: 1 2 3): "

:: 解析选择的运行时
set "RUNTIMES_LIST="
for %%r in (%runtime_selections%) do (
    if "%%r"=="1" set "RUNTIMES_LIST=!RUNTIMES_LIST! win-x64"
    if "%%r"=="2" set "RUNTIMES_LIST=!RUNTIMES_LIST! win-x86"
    if "%%r"=="3" set "RUNTIMES_LIST=!RUNTIMES_LIST! win-arm64"
    if "%%r"=="4" set "RUNTIMES_LIST=!RUNTIMES_LIST! linux-x64"
    if "%%r"=="5" set "RUNTIMES_LIST=!RUNTIMES_LIST! linux-arm64"
    if "%%r"=="6" set "RUNTIMES_LIST=!RUNTIMES_LIST! osx-x64"
    if "%%r"=="7" set "RUNTIMES_LIST=!RUNTIMES_LIST! osx-arm64"
)

if "!RUNTIMES_LIST!"=="" (
    echo [ERROR] 未选择任何运行时！
    pause
    goto MENU
)

echo.
echo [INFO] 将发布以下运行时: !RUNTIMES_LIST!
echo.

:: 获取统一的发布配置
call :GET_PUBLISH_OPTIONS

:: 循环发布每个运行时
set /a count=0
set /a success=0
set /a failed=0

for %%r in (!RUNTIMES_LIST!) do (
    set /a count+=1
    echo.
    echo ============================================
    echo 发布 [!count!]: %%r
    echo ============================================
    
    set "RUNTIME=%%r"
    call :EXECUTE_PUBLISH
    
    if !errorlevel! equ 0 (
        set /a success+=1
    ) else (
        set /a failed+=1
    )
)

echo.
echo ============================================
echo          批量发布完成
echo ============================================
echo 总计: !count! 个运行时
echo 成功: !success! 个
echo 失败: !failed! 个
echo.
pause
goto MENU

:: ==================== 完整流程 ====================
:FULL_PROCESS
echo.
echo ============================================
echo [INFO] 开始执行完整构建流程...
echo ============================================
echo.

echo [STEP 1/4] 清理项目...
dotnet clean %SOLUTION_FILE% --configuration %BUILD_CONFIG%
if %errorlevel% neq 0 goto PROCESS_ERROR

echo.
echo [STEP 2/4] 还原依赖...
dotnet restore %SOLUTION_FILE%
if %errorlevel% neq 0 goto PROCESS_ERROR

echo.
echo [STEP 3/4] 构建项目...
dotnet build %SOLUTION_FILE% --configuration %BUILD_CONFIG% --no-restore
if %errorlevel% neq 0 goto PROCESS_ERROR

echo.
echo [STEP 4/4] 发布项目...
call :GET_PROJECT_INFO
call :GET_RUNTIME_INFO

if "!RUNTIME!"=="multi" (
    echo.
    echo [INFO] 检测到多运行时模式，将进入批量发布流程...
    pause
    goto MULTI_PUBLISH
)

call :GET_PUBLISH_OPTIONS
call :EXECUTE_PUBLISH

echo.
echo ============================================
echo [SUCCESS] 所有步骤完成！
echo ============================================
pause
goto MENU

:PROCESS_ERROR
echo.
echo [ERROR] 流程执行失败！
pause
goto MENU

:: ==================== 运行测试 ====================
:TEST
echo.
echo [INFO] 开始运行测试...
dotnet test %SOLUTION_FILE% --configuration %BUILD_CONFIG% --no-build --verbosity normal
if %errorlevel% neq 0 (
    echo [ERROR] 测试失败！
    pause
    goto MENU
)
echo [SUCCESS] 测试完成！
pause
goto MENU

:: ==================== 打包 NuGet ====================
:PACK
echo.
echo [INFO] 开始打包 NuGet...
dotnet pack %PROJECT_FILE% --configuration %BUILD_CONFIG% --output .\nupkg --no-build
if %errorlevel% neq 0 (
    echo [ERROR] 打包失败！
    pause
    goto MENU
)
echo [SUCCESS] 打包完成！
echo [INFO] 输出目录: .\nupkg
pause
goto MENU

:: ==================== 备份发布文件 ====================
:BACKUP
echo.
echo [INFO] 开始备份发布文件...

if not exist "%OUTPUT_BASE_DIR%" (
    echo [ERROR] 发布目录不存在: %OUTPUT_BASE_DIR%
    pause
    goto MENU
)

:: 创建备份目录
if not exist "%BACKUP_DIR%" mkdir "%BACKUP_DIR%"

:: 生成时间戳
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set datetime=%%I
set TIMESTAMP=%datetime:~0,8%_%datetime:~8,6%

:: 备份文件
set BACKUP_FILE=%BACKUP_DIR%\%PROJECT_NAME%_%TIMESTAMP%.zip
powershell -Command "Compress-Archive -Path '%OUTPUT_BASE_DIR%\*' -DestinationPath '%BACKUP_FILE%' -Force"

if %errorlevel% neq 0 (
    echo [ERROR] 备份失败！
    pause
    goto MENU
)

echo [SUCCESS] 备份完成！
echo [INFO] 备份文件: %BACKUP_FILE%
pause
goto MENU

:: ==================== 修改设置 ====================
:SETTINGS
cls
echo.
echo ============================================
echo            项目设置配置
echo ============================================
echo.
echo 当前设置:
echo [1] 项目名称: %PROJECT_NAME%
echo [2] 解决方案文件: %SOLUTION_FILE%
echo [3] 项目文件: %PROJECT_FILE%
echo [4] 构建配置: %BUILD_CONFIG%
echo [5] 输出基础目录: %OUTPUT_BASE_DIR%
echo.
echo [0] 返回主菜单
echo.
echo ============================================
echo.
set /p setting_choice="请选择要修改的设置 [0-5]: "

if "%setting_choice%"=="1" (
    set /p PROJECT_NAME="请输入项目名称: "
    set SOLUTION_FILE=!PROJECT_NAME!.sln
    set PROJECT_FILE=!PROJECT_NAME!.csproj
    goto SETTINGS
)
if "%setting_choice%"=="2" (
    set /p SOLUTION_FILE="请输入解决方案文件名: "
    goto SETTINGS
)
if "%setting_choice%"=="3" (
    set /p PROJECT_FILE="请输入项目文件名: "
    goto SETTINGS
)
if "%setting_choice%"=="4" (
    echo.
    echo 可选值: Debug, Release
    set /p BUILD_CONFIG="请输入构建配置: "
    goto SETTINGS
)
if "%setting_choice%"=="5" (
    set /p OUTPUT_BASE_DIR="请输入输出基础目录: "
    goto SETTINGS
)
if "%setting_choice%"=="0" goto MENU
goto SETTINGS

:: ==================== 退出 ====================
:END
echo.
echo 感谢使用，再见！
timeout /t 2 >nul
exit /b 0