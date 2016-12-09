﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Decoded message (part 1): {RepetitionDecoder.Decode(Puzzle.Input, CharacterSelectingPolicy.MostCommon)}");
            Console.WriteLine($"Decoded message (part 2): {RepetitionDecoder.Decode(Puzzle.Input, CharacterSelectingPolicy.LeastCommon)}");
        }
    }

    public static class RepetitionDecoder
    {
        public static string Decode(string message, CharacterSelectingPolicy policy)
        {
            int messageLength = -1;
            Dictionary<char, int>[] columnDicts = null;
            foreach (var line in message.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (messageLength == -1)
                {
                    messageLength = line.Length;
                    columnDicts = new Dictionary<char, int>[messageLength];
                }

                for (var i = 0; i < line.Length; i++)
                {
                    if (columnDicts[i] == null)
                    {
                        columnDicts[i] = new Dictionary<char, int>();
                    }

                    var c = line[i];
                    int count;
                    if (!columnDicts[i].TryGetValue(c, out count))
                    {
                        columnDicts[i][c] = 1;
                        continue;
                    }

                    columnDicts[i][c]++;
                }
            }

            Func<Dictionary<char, int>, char> characterSelector = dict => policy == CharacterSelectingPolicy.MostCommon ? dict.OrderByDescending(kvp => kvp.Value).First().Key : dict.OrderBy(kvp => kvp.Value).First().Key;

            return new String(columnDicts.Select(characterSelector).ToArray());
        }
    }

    public enum CharacterSelectingPolicy
    {
        MostCommon,
        LeastCommon
    }

    public static class Puzzle
    {
        public static string Input =
@"bztzdacg
xyjtwqap
arsiwubu
cjwvfwtp
dpdymenf
kbedgnri
gsrrprxn
ojdxafjb
umujtcwg
fcpqbkti
hfhxzhdt
oradqnqo
pjfgpkgz
fagrqjrn
xjkujuks
iisbsyvl
narthccx
sjbnbbxp
ljaemgip
sdlenzfu
zhzlsmgw
sbdndyku
ekcktelc
vxgoapax
bhepszpx
hozlfbdm
fkjgygrj
keutuund
agzrwrzb
mgcucvkn
wiguuvtm
bnyviixq
ybsozbss
cxaqckwv
ulnksnjj
dsrvvmbs
azfnwcdk
hwtwbilm
lieglyxh
vpqfjxcv
sfwxiyqr
hqifjldw
icnsplkz
oxnqizfs
nzfwhwyu
ygaoadfd
dctauvvs
zmtgvtxg
wwzpvoym
cwhcqxvu
rmcvjkzu
yhpwofcb
nulkmdbi
kdppiqlw
awwysfhc
wutwcxvv
haivrowg
mfmsgbsl
fwmphkmm
mjzwenbv
fkvyebad
sprgbbet
mvndpyjx
azwiuvxc
jysrvsaa
dbdhmylh
bgtahasz
rxvnuzdh
abuhtxpr
sicbkalw
ivrdycpj
zhnmrmhl
wauhlrzq
qvdvzigy
exaihdzw
enmyjluj
pzbmvacm
kwiiicwu
caxviljd
fnpjelcd
sekexsoz
yjpdqfte
lbscrmze
hczfunor
hdvlprev
muyeuczq
eyfcvwhv
wnajxlxv
qovjvytl
nrbzwhsf
qgwbxlqh
djvueone
tjbxisce
fwrbhzrk
wesdwdmd
ssumqhxi
kkbxbgyg
ghlmwlkn
nwqprhnc
zothmtwq
xlxhmpvb
wlyckxvt
bmgdhtoc
zkysylxx
paoxzqpo
dpufpfkq
rehmrtqi
zpwyfvdk
kwdpimya
ipnjkoso
mxqxkokf
rsczwsfd
rqrpctlq
geozhplt
ajyatmjk
vkbycpap
pmoftkvg
tzjsvaiy
shoghugz
zbkifgyr
pbkpbmev
mlodbiog
ceemnpgc
rnhmanzi
mkbwwvbx
iwgnsett
utbnohwb
ddxtrdje
mcpjzqws
nrixbssl
tdwjqjgc
pyjyaost
ktslndcw
jseutzlp
aipejffr
iwcjyacs
lshhpbdq
gpenffaf
cukufmfp
ghwjsabp
tmxtxsmw
dhyfbhne
pngepnxe
pojchoea
gtgiogrp
xydkeazh
vqwnnjvx
azjcxxbj
iteaqkgn
nnfhcbhc
oqrlizro
nexqvoak
rglnpjny
ygzhhlqc
mikoaotl
jrhixpef
eikwdvag
wjinncla
ldkfliro
vkmadbkm
loxctpok
iicuxdes
utkriebm
mohvgmkt
fgsaycsy
xmvkgduq
rqhboixq
pgvrtlnh
ztahypot
vmiwyimf
jcgwncls
yiyusdvo
ucqjaxze
bxjgpahy
uginqzlk
nlaxigbi
fpgrsfym
bnanuhpw
uapzqpix
ntebfqhr
defunben
ructvovh
aqhymxge
gdzxxnvd
xauqzcfd
fhrqlybl
erlyajtu
xsqdriep
tjkoykqt
osjeelqi
clofyxny
lhwssjlj
fhaqhrki
cxgocyms
dbfqhwbk
fqpxxtdc
drxiflgp
athtumkv
wpdjptpu
ofqtdaja
fapibvfg
icohxeds
jgmgzzsd
vogagqfx
qelzldjl
qrdlgaqb
woncwuof
sdltdgrl
hwwvgjoz
itrqkyhw
slmehuaw
uxhtcxyc
lsrgmwcr
ligjqlcx
unzkbdhh
vmssbxos
plkfxaqr
zmmeokxo
rgupyiig
psqalimc
zkpdxmny
ctkzxvob
eumydwul
zmzfsjwx
vsdqjfwn
xywlcgzx
yfxinykj
xzfmavcq
uotgqrnu
mhgclvyn
qqetjpeo
bjvmylzy
iruxfcrz
jvldgohm
qvevmjjb
gbobtgrq
iwesuoas
wqkdnzpt
iqhlvjyk
gyqyoolp
wvacvumq
caqzoruh
ftpxpkpr
wvcztqyk
qfqjtgey
croanejb
cbqjdcyz
kxewlwwi
fqarbziw
pepzgqha
cqaeejem
rzezjxhr
ouzyjmdj
sxkmzmqg
txdyoycu
shuzdzke
tyblgetz
troqvtbc
vtkjnqzf
qkzksiui
yvitrrnb
orustrih
dckmcabj
rzkgzwwa
stqdalzw
qlfrvdfx
tbbywjpk
rgmptpyt
oywgrgnf
mczqsszo
bqcllnsw
ryungzti
hsdhzcsq
qlwwnjao
gebhrhss
auelxsel
ooibpinr
hvbwkafc
wyxniwnp
yuniddtp
lkfmtxet
uzvwegaf
mooyfrwz
zbzziowo
udyxjlih
husurpur
oufjezde
dhhqhhvu
ggpiyvdj
pnwosfne
dydoaomg
jigdybwi
jrezqnyp
uqhjltum
mqtfqsez
yebfabmm
vvvvbrta
uvppafsf
sllgpeix
ulgeznra
ifpvbsql
mzdrgkrt
xolsjkcy
azusnhor
hyfnelhw
becqefpj
epyokhod
tqojzdgz
ckjqztth
aweeqxmd
fehhuhve
bjrgsmrc
zmqofcvf
zfjfaomq
bssvmrde
xfdblalt
qhywbzba
mtgsvemd
wvberfyb
mjxkmqul
aytkyusz
sabuotpk
uacviqis
jolhhusp
xbjvogig
gatwogxo
taxbkfxi
fhvejhkr
mlmbhjis
zuezkkhq
cdwcwuvv
jmlprdmv
ydwyooiu
exqatqjs
eunwnucj
hkrnsana
tmfeymay
tcnogxpv
lnlrpnzt
vavbgplk
kjymlakj
tmlseyul
zfowvrum
yzvwvrmw
qntgqett
vwwqkqdt
xlfedyzg
ehxpxwjn
wmijbqgj
idftxkgo
mymigedt
bmouaruq
bxnbmvxz
jqmgqwta
idawdbub
opxennsn
jeotjdhx
sojzqnpa
xouarvap
yjuoninr
vqjogzjl
khzkueji
ndoiuxtm
wifnoizv
afzkeddd
pfyyvihg
pcsrligt
xnsxcyrz
gzququlr
vihfqqdo
lsbsitur
kqrkamdr
peludmha
dfvkfbyu
zscdzqua
lumijqbx
hzkwiitz
yatolkoh
ikbbpwdh
emamjtmc
eqioawcx
bfkrcrfp
ksntxvee
bnslkpji
tblktbpn
gvqfksyi
caplwhtr
jtcllupx
ployzvxi
gygpdyfb
twdijfma
yrzmyoza
gymlehrc
bppycuib
uftzoycx
uxokapsw
jpiwwboy
eciicuyo
hgyuxrfj
hjdhthqg
cexcqher
vsjolhue
odqcfrkk
gkypgguj
xuthrpos
nkzpsbqv
erofbehb
pnxcwqyk
gcwhkxjo
npbnukjp
qmshmgej
txrcdgeo
xttdifgz
vkqihmqf
rlaqduzu
hcjjkhxu
qctdznju
yvekncrn
cajupqki
nccbcgcq
byxxgvjk
zidpfckc
bzeawgwe
omyegefy
ythcjpbj
kcxohucy
wkigkolb
ntvrrpqn
qxcxwtuc
idxsnspd
nvivszfy
tkqlznph
vxnsojrk
kralcfwl
hfkgjwds
wuzgluwn
jwltajll
vpcbpboa
bnrrdjry
uurpkisf
qbjniyrw
einiyjjk
gwmmewnm
erxoycqf
uwdvnnwd
rjqfozcv
nvnjlmum
kuirunnm
moguesgh
luiabxha
xeuaetvy
zcijrciq
dxyawjfe
bxxaytah
firlwpxz
akepeyho
xneohwpu
wrhemsyg
gbafuemn
kzrlkkdd
xsgupnkf
xbqaoosg
dlkuzeqc
hidzsgot
emftjwln
tvwbrngp
habctvij
idzmgcue
flyyevlb
orhdasvb
qwgsvokg
rgpcvtee
jvwyfziw
lzikmdqh
dsyvnfaj
inrlcdst
ehzxjtin
pdnhilac
cvfbcuyz
vtcmfsbh
lnmgmfzw
vovbxstv
opvfvcnx
zlyaoifj
ziumzwqh
phtmspff
jijqudod
jtistrvr
oucmojll
lfjckzhn
fkaodyev
qgspkigk
ypstmrav
kxmxsxrg
egmkleby
ogralaup
klguxwmz
sdpqwdcl
pctkiaws
tvsrfgix
myyrujrq
lzluftpg
rbvscsuk
jfjfdstb
lexiumav
nyvrxexy
jioktbzf
arubkmmm
fnvimawa
dwncnkfi
hyfpywbn
wootqtvb
ktlnzuyh
qpbackvb
reaqetoi
edmnonic
nlpdmqfu
osfsybtk
asnfzlnn
lzsspajo
qanbwzel
lblvbkof
uvurrekd
kshqoiqw
oosxudql
orakdrgn
yotzryse
rpuhmeau
cqchkvbo
ajcaluhy
sabvtxiq
sctoapgf
ihhznfmd
yenlgcmo
dihxrbos
tfusfxad
sdmdfhxa
kodcikxm
cfvvcfum
ynnrmqiw
rbsepvwv
npmdblpf
jgltfwgq
guitdsvy
nfyzuhgv
dgjghspu";
    }
}
