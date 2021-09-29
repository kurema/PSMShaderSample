# PlayStation Mobile用のシェーダーテスト
## あらまし
2013年10月頃に、PlayStation Mobileを始めてみようかなぁ、と思って少し触った時の残骸です。  
シェーダープログラムが結構面白くて、いくらか書いてみました。三次元は難しいので二次元用です。  
漠然とシューティングゲームくらいなら作れるかなぁと思いながら、しかしVitaの実機もPSM対応のAndroidも無かったので放置していました。

そして時は過ぎ2015年3月。PlayStation Mobile終了。  
まぁ、そうだろうなぁと思いましたが、Q&Aにソースコードの配布が許されていたのでここに配布します。

### Q&Aの内容
> Q：自分で開発したPSMアプリケーションのソースコードを配布しても良いですか？  
> A：問題ありません。

https://ja-support.psm.playstation.net/app/answers/detail/a_id/342
## 内容
幾つかのシェーダーのサンプルです。

タイトル | 内容
--- | ---
Simple | 一色表示
DanmakuBasic | 基本的な弾幕です。一回のみ。
DanmakuRepeat | 基本的な弾幕です。繰り返し。
DanmakuRepeatLight | DanmakuRepeatの軽量版。
DanmakuRepeatLimited | DanmakuRepeatの方向制限版。
NoiseColor | テレビ風の色付きノイズ。
StarFloat | 乱数生成にfloatを使った星空です。
StarInteger | 乱数生成にunsigned intを使った星空です。
StarIntegerWave | 星空と波打ちエフェクトの組み合わせです。味方出現時など。
StarIntegerWaveTwinkle | StarIntegerWaveにさらに星を点滅させたものです。
TileBasic | タイルです。
TileBritish | 英国積みタイルです。
## その他
他にやろうと思ったのはこんなものがあります。
* ポストエフェクト
* シェーダーによる高速な文字の表示。
* 2Dマップの表示。

注意した事は整数の乗除算が使えないので代わりにビットシフトで代用した事くらいです。
## ライセンス
MITライセンスです。
