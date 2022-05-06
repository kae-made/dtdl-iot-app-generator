# C# 向け生成コード  
自動生成により、選択した種別に応じて、以下のコード群が生成される。  

### Device App 形式  
- AppIoTData.cs
- IIoTApp.cs
- IoTApp.cs
- IoTApp.code.cs
- Program.cs
- <i>MyApp</i>AppConnector.cs
- <i>MyApp</i>.csproj
- iot-app-config.yaml

### Service 形式  
- AppIoTData.cs
- IIoTApp.cs
- IoTApp.cs
- IoTApp.code.cs
- Worker.cs
- Program.cs
- <i>MyApp</i>AppConnector.cs
- <i>MyApp</i>.csproj
- iot-app-config.yaml

### IoT Edge Module 形式  
- AppIoTData.cs
- IIoTApp.cs
- IoTApp.cs
- IoTApp.code.cs
- Program.cs
- <i>MyApp</i>AppConnector.cs
- <i>MyApp</i>.csproj
- iot-app-config.yaml
- Docker.<i>arch</i>
- Docker.<i>arch</i>.debug
- module.json

※ <i>MyApp</i> は、Project Name で置き換わる。  
※ <i>arch</i> は、amd64、arm32v7、arm64v8、windows-amd64  

### IoTApp.code.cs  
このファイルは、ユーザー固有のロジックを記述するためのファイルである。このファイル以外は、基本的にユーザー編集は禁止とする。  
このファイルには、
- C2D メッセージ受信時にコールされるメソッド - async Task ReceivedC2DDataAsync(Message msg)
- Desired Properties が更新された時にコールされるメソッド - async Task UpdatedDTDesiredPropertiesAsync(AppDTDesiredProperties dp)
- DTDL PnP 内で Command として定義された DirectMethod が起動された時の処理を記述する為のメソッド群 - async Task<string> <i>CommandName</i>(string payload)
- Azure IoT Hub 接続前に行う初期化処理記述用メソッド - UserPreInitializeAsync()
- Azure IoT Hub 接続後に行う初期化処理記述用メソッド - UserPostInitializeAsync()
- 初期化終了後の様々な IoT アプリ関連処理を記述する為のメソッド - DoWorkAsync()
- IoT アプリ関連処理終了後の、Azure IoT Hub 接続クローズ前に行う終了処理記述用メソッド - UserPreTerminateAsync()
- IoT アプリ関連処理終了後の、Azure IoT Hub 接続クローズ後に行う終了処理記述用メソッド

のメソッド群が定義された IoTApp という名前のクラス定義が生成されている。ユーザーはそれぞれの場所に必要なコードを記述するだけでよい。  
IoTApp クラスには、IoTClient という型の iotClient というメンバー変数を持っている。ユーザーコードを記述するメソッド内で、iotClient を参照可能であり、以下の通り、IoT Hub への D2C メッセージへの送信や、Reported Properties の更新等が行える。  
```C#
Task IoTClient.SendD2CMessageAsync(IoTDataWithProperties data, string outputPort=null)  
```
D2C メッセージを送信する。AppIoTData.cs に DTDL PnP で定義されている Telemetry 群から定義した D2CData という型が定義され、IoTAppクラスのメンバー変数に、sensingData というプロパティが定義されているので、センサー等で計測したデータ群を sensingData にセットして、本メソッドの引数として利用すると楽に送信できる。  
※ outputPort は、IoT Edge Module の場合だけ利用可能  

IoT アプリシナリオでは、定期的にデータを送信する場合が多いので、その様なシナリオ向けに以下の3つのメソッドが用意されている。  
- StartSendD2CMessageAsync()
- UpdateD2CDataAsync()
- StopSendD2CMessage()

```C#
Task StartSendD2CMessageAsync(TimeSpan interval, string outputPort=null)
```
定期的送信を開始する。送信するデータは、IoTData.sensingData を使用する。  

```C#
Task UpdateD2CDataAsync(IoTDataWithProperties data) 
```
定期的に送信する際に使用する D2C データを更新する。このメソッド内ではマルチスレッド排他処理が行われるので、特に排他を考慮する必要はない。  

```C#
void StopSendD2CMessage()
```
定期的送信を終了する。  

IoTClient は、D2C メッセージでは送信出来ないサイズの大きなデータを送信するためのメソッドも提供している。実行形式が、デバイスアプリ、サービスの場合は、特に設定は必要はないが、IoT Edge Module の場合は、iot-app-config.yaml 内で、Blob on Edge のモジュール名、ローカルアカウント、ローカルアカウントキーを設定する必要がある。  
```C#
Task UploadLargeDataAsync(string blobName, Stream data)
```

## 接続設定  
実行形式が、デバイスアプリ、サービスの場合は、iot-app-config.yaml に必要な項目を設定することで、Azure IoT Hub への接続の際の、認証とプロトコルを指定可能になっている。
加えて、Device Provisioning Service の利用も可能であり、こちらも、認証とプロトコルを指定可能になっている。  

