:: Start Server
ECHO 'Starting Server...'
start cmd /c D:\GitHub\NetcodeGP\NetcodeGP\Builds\Server\NetcodeGP_StartServer_S.lnk
ECHO 'Server Started Successfully.'

:: Start Client
ECHO 'Starting Client...'
start cmd /c D:\GitHub\NetcodeGP\NetcodeGP\Builds\Client\NetcodeGP_StartClient_S.lnk
ECHO 'Client Started Successfully.'

:: Wait for the user to close the console
SET /P AWAITKEY=Press Enter to continue...
