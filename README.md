# 防衛シューティング
- 開発者：張 皓嵐
- 学校の制作実習で作ったゲームです。
- Unityで作りました。
- 熊に襲われないようにするゲームです。
- 熊の攻撃力は熊のHPと同じです。
- プレイヤーのHPが0になった次第ゲーム終了します。
- 実行ファイル：[https://github.com/loaqloaqloaq/Defence-Shooting-Game/releases](https://github.com/loaqloaqloaq/Defence-Shooting-Game/releases)


# 操作方法：
- マウスを押し続けるとチャージし、放すと射撃します。
- くまを倒すと経験値もらいます、経験値が溜まるとレベルアップします。
- レベルアップする時、画面のボタンをクリックすることでチャージ速度や弾丸の威力を高めることができます。


# 独自に実装した要素・PR ポイント：
- Universal RPを使って、時間変換と影を実装しました。
- レベルアップする時、ゲームを一時停止するようにしました。キャラクターの動画も一時停止するようにしました。
- ゲームは段々と難しくなり、プレイヤーがチャレンジできるようになるため、熊のHPが次第に増えます。
- 熊の動画に合わせるために、攻撃はInvokeを使いました。
- エンディング画面でScroll viewでランキングを表示します。
- ランキングは保存するために、外部ファイルに書き込まれています。
- エンディング画面に入った際に、まず外部ファイルを読み込み、その内容を元に配列を作成します。その後今回の成績を配列に追加し、配列をソートします。

# 使用したしたリソース：
1. BGM:
- https://maou.audio/bgm_fantasy13/ タイトル画面のBGM
- https://maou.audio/bgm_fantasy08/ メインゲームのBGM
- https://maou.audio/bgm_fantasy14/ エンディング画面のBGM

2. 効果音:
- https://maou.audio/se_battle_explosion06/ 爆発音
- https://maou.audio/se_system49/ ボタンを押す音
- https://maou.audio/se_system43/ 射撃音

*他のリソースは学校で配布したリソースです。
