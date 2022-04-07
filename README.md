# DTDL IoT App Generator
This application generates IoT application framework which can collaborate with Azure IoT Hub from IoT PnP DTDL file.
DTDL で定義された IoT Plug and Play モデルを元に、Azure IoT Hub と連携する IoT アプリケーションの雛形を生成する。  
生成する際、アプリケーションの形式として、
- 実行ファイル形式
- Docker Image 形式  

のどちらかを、実装する言語に応じて、選択できる。また、実行形式の場合は、通常のアプリケーション形式による実行、または、システムサービス形式による実行のいずれかを選択できる。  
加えて、実装で使用するプログラミング言語も以下のどれかから選択可能である。  
- C/C++
- C#
- Python

C/C++ の場合で、かつ、実行ファイル形式の場合は、Azure IoT Device SDK for C、もしくは、Azure SDK for Embedded C のいずれかを選択できる。
選択する言語とアプリケーション形式の関係を表にまとめる。  

|プログラミング言語|通常アプリ形式|システムサービス形式|Docker Image|備考|
|-|-|-|-|-|
|C/C++|〇|☓|☓|Azure IoT Device SDK for C または Azure SDK for Embedded Cのいずれかを選択可|
|C#|〇|〇|〇|通常アプリ形式の場合は.NET Coreを使ったデスクトップアプリ|
|Python|〇|☓|〇||

Generator によって、以下の機能を実装するコードが生成される。 
|機能|調整項目|ユーザーアプリ側で実装|
|-|-|-|
|Azure IoT Hub との接続|接続文字列、または、証明書|認証情報の供給|
|Device 2 Cloud メッセージ送信|定期的送信、または、不定期送信|送信用データの準備と送信指示|
|Cloud 2 Device メッセージ受信| |受信時のコールバックルーチン|
|Direct Method Invocation 受信と返信|シングルスレッド、マルチスレッド指定 |受信時のコールバックルーチン|
|Device Twins 保持用データモデルと参照| | |
|Device Twins Desired Properties 受信| |受信時のコールバックルーチン|
|Device Twins Reported Properties 更新| |書き込み用データの準備と更新指示|
|実行ログ蓄積用コード|||
|256KB以上のデータアップロード| |アップロード用データの準備とアップロード指示|

