using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000250 RID: 592
	internal sealed class RTFData
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x000C1770 File Offset: 0x000BF970
		public static short Hash(byte[] chars, int off, int len)
		{
			short num = 0;
			while (len != 0)
			{
				byte b = chars[off];
				num = (short)((((int)num << 3) + (num >> 6) ^ (int)b) % 299);
				len--;
				off++;
			}
			return num;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000C17A4 File Offset: 0x000BF9A4
		public static short AddHash(short hash, byte ch)
		{
			return (short)((((int)hash << 3) + (hash >> 6) ^ (int)ch) % 299);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000C17B6 File Offset: 0x000BF9B6
		private RTFData()
		{
		}

		// Token: 0x04001BA1 RID: 7073
		public const int ID__unknownKeyword = 0;

		// Token: 0x04001BA2 RID: 7074
		public const int ID__ignorableDest = 1;

		// Token: 0x04001BA3 RID: 7075
		public const int ID__formulaChar = 2;

		// Token: 0x04001BA4 RID: 7076
		public const int ID__indexSubentry = 3;

		// Token: 0x04001BA5 RID: 7077
		public const int ID_aul = 4;

		// Token: 0x04001BA6 RID: 7078
		public const int ID_ulw = 5;

		// Token: 0x04001BA7 RID: 7079
		public const int ID_pichgoal = 6;

		// Token: 0x04001BA8 RID: 7080
		public const int ID_trbrdrb = 7;

		// Token: 0x04001BA9 RID: 7081
		public const int ID_leveltext = 8;

		// Token: 0x04001BAA RID: 7082
		public const int ID_listlevel = 9;

		// Token: 0x04001BAB RID: 7083
		public const int ID_trbrdrh = 10;

		// Token: 0x04001BAC RID: 7084
		public const int ID_brdrengrave = 11;

		// Token: 0x04001BAD RID: 7085
		public const int ID_trbrdrl = 12;

		// Token: 0x04001BAE RID: 7086
		public const int ID_irow = 13;

		// Token: 0x04001BAF RID: 7087
		public const int ID_brdrtriple = 14;

		// Token: 0x04001BB0 RID: 7088
		public const int ID_footer = 15;

		// Token: 0x04001BB1 RID: 7089
		public const int ID_trbrdrr = 16;

		// Token: 0x04001BB2 RID: 7090
		public const int ID_caps = 17;

		// Token: 0x04001BB3 RID: 7091
		public const int ID_fscript = 18;

		// Token: 0x04001BB4 RID: 7092
		public const int ID_uldash = 19;

		// Token: 0x04001BB5 RID: 7093
		public const int ID_expndtw = 20;

		// Token: 0x04001BB6 RID: 7094
		public const int ID_pnucrm = 21;

		// Token: 0x04001BB7 RID: 7095
		public const int ID_trbrdrt = 22;

		// Token: 0x04001BB8 RID: 7096
		public const int ID_brdrwavydb = 23;

		// Token: 0x04001BB9 RID: 7097
		public const int ID_header = 24;

		// Token: 0x04001BBA RID: 7098
		public const int ID_trbrdrv = 25;

		// Token: 0x04001BBB RID: 7099
		public const int ID_embo = 26;

		// Token: 0x04001BBC RID: 7100
		public const int ID_pnindent = 27;

		// Token: 0x04001BBD RID: 7101
		public const int ID_zwj = 28;

		// Token: 0x04001BBE RID: 7102
		public const int ID_field = 29;

		// Token: 0x04001BBF RID: 7103
		public const int ID_fnil = 30;

		// Token: 0x04001BC0 RID: 7104
		public const int ID_link = 31;

		// Token: 0x04001BC1 RID: 7105
		public const int ID_disabled = 32;

		// Token: 0x04001BC2 RID: 7106
		public const int ID_footnote = 33;

		// Token: 0x04001BC3 RID: 7107
		public const int ID_fcharset = 34;

		// Token: 0x04001BC4 RID: 7108
		public const int ID_mac = 35;

		// Token: 0x04001BC5 RID: 7109
		public const int ID_pnucltr = 36;

		// Token: 0x04001BC6 RID: 7110
		public const int ID_fbidis = 37;

		// Token: 0x04001BC7 RID: 7111
		public const int ID_lquote = 38;

		// Token: 0x04001BC8 RID: 7112
		public const int ID_macpict = 39;

		// Token: 0x04001BC9 RID: 7113
		public const int ID_row = 40;

		// Token: 0x04001BCA RID: 7114
		public const int ID_rtlrow = 41;

		// Token: 0x04001BCB RID: 7115
		public const int ID_fprq = 42;

		// Token: 0x04001BCC RID: 7116
		public const int ID_picprop = 43;

		// Token: 0x04001BCD RID: 7117
		public const int ID_levelstartat = 44;

		// Token: 0x04001BCE RID: 7118
		public const int ID_pich = 45;

		// Token: 0x04001BCF RID: 7119
		public const int ID_brdrwavy = 46;

		// Token: 0x04001BD0 RID: 7120
		public const int ID_bin = 47;

		// Token: 0x04001BD1 RID: 7121
		public const int ID_line = 48;

		// Token: 0x04001BD2 RID: 7122
		public const int ID_fmodern = 49;

		// Token: 0x04001BD3 RID: 7123
		public const int ID_pict = 50;

		// Token: 0x04001BD4 RID: 7124
		public const int ID_pnlvlblt = 51;

		// Token: 0x04001BD5 RID: 7125
		public const int ID_brdrdashsm = 52;

		// Token: 0x04001BD6 RID: 7126
		public const int ID_clcfpat = 53;

		// Token: 0x04001BD7 RID: 7127
		public const int ID_list = 54;

		// Token: 0x04001BD8 RID: 7128
		public const int ID_nestrow = 55;

		// Token: 0x04001BD9 RID: 7129
		public const int ID_brdrbtw = 56;

		// Token: 0x04001BDA RID: 7130
		public const int ID_picw = 57;

		// Token: 0x04001BDB RID: 7131
		public const int ID_cbpat = 58;

		// Token: 0x04001BDC RID: 7132
		public const int ID_rtlmark = 59;

		// Token: 0x04001BDD RID: 7133
		public const int ID_deflang = 60;

		// Token: 0x04001BDE RID: 7134
		public const int ID_ulhwave = 61;

		// Token: 0x04001BDF RID: 7135
		public const int ID_pnaiud = 62;

		// Token: 0x04001BE0 RID: 7136
		public const int ID_ulldash = 63;

		// Token: 0x04001BE1 RID: 7137
		public const int ID_brdrdashdotstr = 64;

		// Token: 0x04001BE2 RID: 7138
		public const int ID_nesttableprops = 65;

		// Token: 0x04001BE3 RID: 7139
		public const int ID_trleft = 66;

		// Token: 0x04001BE4 RID: 7140
		public const int ID_bghoriz = 67;

		// Token: 0x04001BE5 RID: 7141
		public const int ID_par = 68;

		// Token: 0x04001BE6 RID: 7142
		public const int ID_keepn = 69;

		// Token: 0x04001BE7 RID: 7143
		public const int ID_pnordt = 70;

		// Token: 0x04001BE8 RID: 7144
		public const int ID_lang = 71;

		// Token: 0x04001BE9 RID: 7145
		public const int ID_fbidi = 72;

		// Token: 0x04001BEA RID: 7146
		public const int ID_lastrow = 73;

		// Token: 0x04001BEB RID: 7147
		public const int ID_bullet = 74;

		// Token: 0x04001BEC RID: 7148
		public const int ID_sectd = 75;

		// Token: 0x04001BED RID: 7149
		public const int ID_ul = 76;

		// Token: 0x04001BEE RID: 7150
		public const int ID_pnlcltr = 77;

		// Token: 0x04001BEF RID: 7151
		public const int ID_clvmrg = 78;

		// Token: 0x04001BF0 RID: 7152
		public const int ID_shad = 79;

		// Token: 0x04001BF1 RID: 7153
		public const int ID_brdrdash = 80;

		// Token: 0x04001BF2 RID: 7154
		public const int ID_uc = 81;

		// Token: 0x04001BF3 RID: 7155
		public const int ID_highlight = 82;

		// Token: 0x04001BF4 RID: 7156
		public const int ID_htmlbase = 83;

		// Token: 0x04001BF5 RID: 7157
		public const int ID_pncnum = 84;

		// Token: 0x04001BF6 RID: 7158
		public const int ID_ud = 85;

		// Token: 0x04001BF7 RID: 7159
		public const int ID_pnstart = 86;

		// Token: 0x04001BF8 RID: 7160
		public const int ID_adeff = 87;

		// Token: 0x04001BF9 RID: 7161
		public const int ID_blue = 88;

		// Token: 0x04001BFA RID: 7162
		public const int ID_brdrdb = 89;

		// Token: 0x04001BFB RID: 7163
		public const int ID_brdrframe = 90;

		// Token: 0x04001BFC RID: 7164
		public const int ID_taprtl = 91;

		// Token: 0x04001BFD RID: 7165
		public const int ID_comment = 92;

		// Token: 0x04001BFE RID: 7166
		public const int ID_froman = 93;

		// Token: 0x04001BFF RID: 7167
		public const int ID_fdecor = 94;

		// Token: 0x04001C00 RID: 7168
		public const int ID_dbch = 95;

		// Token: 0x04001C01 RID: 7169
		public const int ID_up = 96;

		// Token: 0x04001C02 RID: 7170
		public const int ID_brdroutset = 97;

		// Token: 0x04001C03 RID: 7171
		public const int ID_clvertalb = 98;

		// Token: 0x04001C04 RID: 7172
		public const int ID_striked = 99;

		// Token: 0x04001C05 RID: 7173
		public const int ID_clvertalc = 100;

		// Token: 0x04001C06 RID: 7174
		public const int ID_itap = 101;

		// Token: 0x04001C07 RID: 7175
		public const int ID_pnlvl = 102;

		// Token: 0x04001C08 RID: 7176
		public const int ID_clftsWidth = 103;

		// Token: 0x04001C09 RID: 7177
		public const int ID_upr = 104;

		// Token: 0x04001C0A RID: 7178
		public const int ID_nestcell = 105;

		// Token: 0x04001C0B RID: 7179
		public const int ID_pnord = 106;

		// Token: 0x04001C0C RID: 7180
		public const int ID_protect = 107;

		// Token: 0x04001C0D RID: 7181
		public const int ID_pc = 108;

		// Token: 0x04001C0E RID: 7182
		public const int ID_b = 109;

		// Token: 0x04001C0F RID: 7183
		public const int ID_deftab = 110;

		// Token: 0x04001C10 RID: 7184
		public const int ID_qj = 111;

		// Token: 0x04001C11 RID: 7185
		public const int ID_ql = 112;

		// Token: 0x04001C12 RID: 7186
		public const int ID_f = 113;

		// Token: 0x04001C13 RID: 7187
		public const int ID_sp = 114;

		// Token: 0x04001C14 RID: 7188
		public const int ID_i = 115;

		// Token: 0x04001C15 RID: 7189
		public const int ID_qc = 116;

		// Token: 0x04001C16 RID: 7190
		public const int ID_revised = 117;

		// Token: 0x04001C17 RID: 7191
		public const int ID_trpaddr = 118;

		// Token: 0x04001C18 RID: 7192
		public const int ID_cell = 119;

		// Token: 0x04001C19 RID: 7193
		public const int ID_qd = 120;

		// Token: 0x04001C1A RID: 7194
		public const int ID_trpaddt = 121;

		// Token: 0x04001C1B RID: 7195
		public const int ID_brdrthtnlg = 122;

		// Token: 0x04001C1C RID: 7196
		public const int ID_pn = 123;

		// Token: 0x04001C1D RID: 7197
		public const int ID_sv = 124;

		// Token: 0x04001C1E RID: 7198
		public const int ID_brsp = 125;

		// Token: 0x04001C1F RID: 7199
		public const int ID_tab = 126;

		// Token: 0x04001C20 RID: 7200
		public const int ID_trgaph = 127;

		// Token: 0x04001C21 RID: 7201
		public const int ID_shp = 128;

		// Token: 0x04001C22 RID: 7202
		public const int ID_s = 129;

		// Token: 0x04001C23 RID: 7203
		public const int ID_sl = 130;

		// Token: 0x04001C24 RID: 7204
		public const int ID_trpaddl = 131;

		// Token: 0x04001C25 RID: 7205
		public const int ID_brdrthtnmg = 132;

		// Token: 0x04001C26 RID: 7206
		public const int ID_u = 133;

		// Token: 0x04001C27 RID: 7207
		public const int ID_ltrch = 134;

		// Token: 0x04001C28 RID: 7208
		public const int ID_sn = 135;

		// Token: 0x04001C29 RID: 7209
		public const int ID_v = 136;

		// Token: 0x04001C2A RID: 7210
		public const int ID_hich = 137;

		// Token: 0x04001C2B RID: 7211
		public const int ID_ri = 138;

		// Token: 0x04001C2C RID: 7212
		public const int ID_sa = 139;

		// Token: 0x04001C2D RID: 7213
		public const int ID_qs = 140;

		// Token: 0x04001C2E RID: 7214
		public const int ID_qr = 141;

		// Token: 0x04001C2F RID: 7215
		public const int ID_sb = 142;

		// Token: 0x04001C30 RID: 7216
		public const int ID_trcbpat = 143;

		// Token: 0x04001C31 RID: 7217
		public const int ID_trpaddb = 144;

		// Token: 0x04001C32 RID: 7218
		public const int ID_cfpat = 145;

		// Token: 0x04001C33 RID: 7219
		public const int ID_keep = 146;

		// Token: 0x04001C34 RID: 7220
		public const int ID_bgvert = 147;

		// Token: 0x04001C35 RID: 7221
		public const int ID_red = 148;

		// Token: 0x04001C36 RID: 7222
		public const int ID_deflangfe = 149;

		// Token: 0x04001C37 RID: 7223
		public const int ID_ululdbwave = 150;

		// Token: 0x04001C38 RID: 7224
		public const int ID_trrh = 151;

		// Token: 0x04001C39 RID: 7225
		public const int ID__hyphen = 152;

		// Token: 0x04001C3A RID: 7226
		public const int ID_htmlrtf = 153;

		// Token: 0x04001C3B RID: 7227
		public const int ID_picwgoal = 154;

		// Token: 0x04001C3C RID: 7228
		public const int ID_uldashdd = 155;

		// Token: 0x04001C3D RID: 7229
		public const int ID_brdrtnthsg = 156;

		// Token: 0x04001C3E RID: 7230
		public const int ID_objattph = 157;

		// Token: 0x04001C3F RID: 7231
		public const int ID_bgdkbdiag = 158;

		// Token: 0x04001C40 RID: 7232
		public const int ID_uldb = 159;

		// Token: 0x04001C41 RID: 7233
		public const int ID_clmrg = 160;

		// Token: 0x04001C42 RID: 7234
		public const int ID_clpadr = 161;

		// Token: 0x04001C43 RID: 7235
		public const int ID_outl = 162;

		// Token: 0x04001C44 RID: 7236
		public const int ID_clpadt = 163;

		// Token: 0x04001C45 RID: 7237
		public const int ID_fcs = 164;

		// Token: 0x04001C46 RID: 7238
		public const int ID_ansicpg = 165;

		// Token: 0x04001C47 RID: 7239
		public const int ID_shpinst = 166;

		// Token: 0x04001C48 RID: 7240
		public const int ID_brdrcf = 167;

		// Token: 0x04001C49 RID: 7241
		public const int ID_sect = 168;

		// Token: 0x04001C4A RID: 7242
		public const int ID_afs = 169;

		// Token: 0x04001C4B RID: 7243
		public const int ID_plain = 170;

		// Token: 0x04001C4C RID: 7244
		public const int ID_brdrinset = 171;

		// Token: 0x04001C4D RID: 7245
		public const int ID_clpadl = 172;

		// Token: 0x04001C4E RID: 7246
		public const int ID_listid = 173;

		// Token: 0x04001C4F RID: 7247
		public const int ID_acf = 174;

		// Token: 0x04001C50 RID: 7248
		public const int ID_fonttbl = 175;

		// Token: 0x04001C51 RID: 7249
		public const int ID_rtlpar = 176;

		// Token: 0x04001C52 RID: 7250
		public const int ID_htmltag = 177;

		// Token: 0x04001C53 RID: 7251
		public const int ID_ulthdashd = 178;

		// Token: 0x04001C54 RID: 7252
		public const int ID_clpadb = 179;

		// Token: 0x04001C55 RID: 7253
		public const int ID_scaps = 180;

		// Token: 0x04001C56 RID: 7254
		public const int ID_clbrdrr = 181;

		// Token: 0x04001C57 RID: 7255
		public const int ID_shading = 182;

		// Token: 0x04001C58 RID: 7256
		public const int ID_trqr = 183;

		// Token: 0x04001C59 RID: 7257
		public const int ID_pard = 184;

		// Token: 0x04001C5A RID: 7258
		public const int ID_pnfs = 185;

		// Token: 0x04001C5B RID: 7259
		public const int ID_clbrdrt = 186;

		// Token: 0x04001C5C RID: 7260
		public const int ID_bgdkvert = 187;

		// Token: 0x04001C5D RID: 7261
		public const int ID_brdrl = 188;

		// Token: 0x04001C5E RID: 7262
		public const int ID_ltrmark = 189;

		// Token: 0x04001C5F RID: 7263
		public const int ID_ulthldash = 190;

		// Token: 0x04001C60 RID: 7264
		public const int ID_intbl = 191;

		// Token: 0x04001C61 RID: 7265
		public const int ID_bgbdiag = 192;

		// Token: 0x04001C62 RID: 7266
		public const int ID_bkmkstart = 193;

		// Token: 0x04001C63 RID: 7267
		public const int ID_brdrb = 194;

		// Token: 0x04001C64 RID: 7268
		public const int ID_clbrdrl = 195;

		// Token: 0x04001C65 RID: 7269
		public const int ID_clbrdrb = 196;

		// Token: 0x04001C66 RID: 7270
		public const int ID_pagebb = 197;

		// Token: 0x04001C67 RID: 7271
		public const int ID_strike = 198;

		// Token: 0x04001C68 RID: 7272
		public const int ID_clvmgf = 199;

		// Token: 0x04001C69 RID: 7273
		public const int ID_trowd = 200;

		// Token: 0x04001C6A RID: 7274
		public const int ID_info = 201;

		// Token: 0x04001C6B RID: 7275
		public const int ID_ldblquote = 202;

		// Token: 0x04001C6C RID: 7276
		public const int ID_listoverride = 203;

		// Token: 0x04001C6D RID: 7277
		public const int ID_trqc = 204;

		// Token: 0x04001C6E RID: 7278
		public const int ID_ilvl = 205;

		// Token: 0x04001C6F RID: 7279
		public const int ID_li = 206;

		// Token: 0x04001C70 RID: 7280
		public const int ID_zwnj = 207;

		// Token: 0x04001C71 RID: 7281
		public const int ID_listsimple = 208;

		// Token: 0x04001C72 RID: 7282
		public const int ID_brdrdashdd = 209;

		// Token: 0x04001C73 RID: 7283
		public const int ID_fname = 210;

		// Token: 0x04001C74 RID: 7284
		public const int ID_ulnone = 211;

		// Token: 0x04001C75 RID: 7285
		public const int ID_bkmkend = 212;

		// Token: 0x04001C76 RID: 7286
		public const int ID_clwWidth = 213;

		// Token: 0x04001C77 RID: 7287
		public const int ID_adeflang = 214;

		// Token: 0x04001C78 RID: 7288
		public const int ID_brdrr = 215;

		// Token: 0x04001C79 RID: 7289
		public const int ID_brdrs = 216;

		// Token: 0x04001C7A RID: 7290
		public const int ID_pniroha = 217;

		// Token: 0x04001C7B RID: 7291
		public const int ID_brdrt = 218;

		// Token: 0x04001C7C RID: 7292
		public const int ID_ls = 219;

		// Token: 0x04001C7D RID: 7293
		public const int ID_brdrw = 220;

		// Token: 0x04001C7E RID: 7294
		public const int ID_irowband = 221;

		// Token: 0x04001C7F RID: 7295
		public const int ID_loch = 222;

		// Token: 0x04001C80 RID: 7296
		public const int ID_pnf = 223;

		// Token: 0x04001C81 RID: 7297
		public const int ID_slmult = 224;

		// Token: 0x04001C82 RID: 7298
		public const int ID_ulthdash = 225;

		// Token: 0x04001C83 RID: 7299
		public const int ID_bgdkdcross = 226;

		// Token: 0x04001C84 RID: 7300
		public const int ID_pntxta = 227;

		// Token: 0x04001C85 RID: 7301
		public const int ID_cellx = 228;

		// Token: 0x04001C86 RID: 7302
		public const int ID_pnlvlcont = 229;

		// Token: 0x04001C87 RID: 7303
		public const int ID_headerl = 230;

		// Token: 0x04001C88 RID: 7304
		public const int ID_jclisttab = 231;

		// Token: 0x04001C89 RID: 7305
		public const int ID_endash = 232;

		// Token: 0x04001C8A RID: 7306
		public const int ID_pntxtb = 233;

		// Token: 0x04001C8B RID: 7307
		public const int ID_bgfdiag = 234;

		// Token: 0x04001C8C RID: 7308
		public const int ID_brdrbar = 235;

		// Token: 0x04001C8D RID: 7309
		public const int ID_bgdkfdiag = 236;

		// Token: 0x04001C8E RID: 7310
		public const int ID_trwWidth = 237;

		// Token: 0x04001C8F RID: 7311
		public const int ID_alang = 238;

		// Token: 0x04001C90 RID: 7312
		public const int ID_brdrtnthtnlg = 239;

		// Token: 0x04001C91 RID: 7313
		public const int ID_cpg = 240;

		// Token: 0x04001C92 RID: 7314
		public const int ID_headerf = 241;

		// Token: 0x04001C93 RID: 7315
		public const int ID_ltrrow = 242;

		// Token: 0x04001C94 RID: 7316
		public const int ID_brdrtnthtnsg = 243;

		// Token: 0x04001C95 RID: 7317
		public const int ID_bgdkhoriz = 244;

		// Token: 0x04001C96 RID: 7318
		public const int ID_dropcapt = 245;

		// Token: 0x04001C97 RID: 7319
		public const int ID_listtable = 246;

		// Token: 0x04001C98 RID: 7320
		public const int ID_brdrtnthtnmg = 247;

		// Token: 0x04001C99 RID: 7321
		public const int ID_pnlcrm = 248;

		// Token: 0x04001C9A RID: 7322
		public const int ID_pndecd = 249;

		// Token: 0x04001C9B RID: 7323
		public const int ID_leveljc = 250;

		// Token: 0x04001C9C RID: 7324
		public const int ID_brdrtnthmg = 251;

		// Token: 0x04001C9D RID: 7325
		public const int ID_colortbl = 252;

		// Token: 0x04001C9E RID: 7326
		public const int ID_headerr = 253;

		// Token: 0x04001C9F RID: 7327
		public const int ID_brdrth = 254;

		// Token: 0x04001CA0 RID: 7328
		public const int ID_levelpicture = 255;

		// Token: 0x04001CA1 RID: 7329
		public const int ID_brdrtnthlg = 256;

		// Token: 0x04001CA2 RID: 7330
		public const int ID_listtext = 257;

		// Token: 0x04001CA3 RID: 7331
		public const int ID_footerr = 258;

		// Token: 0x04001CA4 RID: 7332
		public const int ID_clmgf = 259;

		// Token: 0x04001CA5 RID: 7333
		public const int ID_bgdcross = 260;

		// Token: 0x04001CA6 RID: 7334
		public const int ID_brdrthtnsg = 261;

		// Token: 0x04001CA7 RID: 7335
		public const int ID_sub = 262;

		// Token: 0x04001CA8 RID: 7336
		public const int ID_rdblquote = 263;

		// Token: 0x04001CA9 RID: 7337
		public const int ID_nosupersub = 264;

		// Token: 0x04001CAA RID: 7338
		public const int ID_fs = 265;

		// Token: 0x04001CAB RID: 7339
		public const int ID_ftech = 266;

		// Token: 0x04001CAC RID: 7340
		public const int ID_rtf = 267;

		// Token: 0x04001CAD RID: 7341
		public const int ID_falt = 268;

		// Token: 0x04001CAE RID: 7342
		public const int ID_fldrslt = 269;

		// Token: 0x04001CAF RID: 7343
		public const int ID_ltrpar = 270;

		// Token: 0x04001CB0 RID: 7344
		public const int ID_urtf = 271;

		// Token: 0x04001CB1 RID: 7345
		public const int ID_pncard = 272;

		// Token: 0x04001CB2 RID: 7346
		public const int ID_footerf = 273;

		// Token: 0x04001CB3 RID: 7347
		public const int ID_pndec = 274;

		// Token: 0x04001CB4 RID: 7348
		public const int ID_dn = 275;

		// Token: 0x04001CB5 RID: 7349
		public const int ID_brdrdashd = 276;

		// Token: 0x04001CB6 RID: 7350
		public const int ID_footerl = 277;

		// Token: 0x04001CB7 RID: 7351
		public const int ID_uldashd = 278;

		// Token: 0x04001CB8 RID: 7352
		public const int ID_ulwave = 279;

		// Token: 0x04001CB9 RID: 7353
		public const int ID_deff = 280;

		// Token: 0x04001CBA RID: 7354
		public const int ID_langfe = 281;

		// Token: 0x04001CBB RID: 7355
		public const int ID_bgdkcross = 282;

		// Token: 0x04001CBC RID: 7356
		public const int ID_nonesttables = 283;

		// Token: 0x04001CBD RID: 7357
		public const int ID_fi = 284;

		// Token: 0x04001CBE RID: 7358
		public const int ID_chcbpat = 285;

		// Token: 0x04001CBF RID: 7359
		public const int ID_rquote = 286;

		// Token: 0x04001CC0 RID: 7360
		public const int ID_rtlch = 287;

		// Token: 0x04001CC1 RID: 7361
		public const int ID_pndbnum = 288;

		// Token: 0x04001CC2 RID: 7362
		public const int ID_brdrsh = 289;

		// Token: 0x04001CC3 RID: 7363
		public const int ID_pnaiu = 290;

		// Token: 0x04001CC4 RID: 7364
		public const int ID_ai = 291;

		// Token: 0x04001CC5 RID: 7365
		public const int ID_fromhtml = 292;

		// Token: 0x04001CC6 RID: 7366
		public const int ID_trhdr = 293;

		// Token: 0x04001CC7 RID: 7367
		public const int ID_ulhair = 294;

		// Token: 0x04001CC8 RID: 7368
		public const int ID_emdash = 295;

		// Token: 0x04001CC9 RID: 7369
		public const int ID_levelnfc = 296;

		// Token: 0x04001CCA RID: 7370
		public const int ID_deleted = 297;

		// Token: 0x04001CCB RID: 7371
		public const int ID_dropcapli = 298;

		// Token: 0x04001CCC RID: 7372
		public const int ID_ulth = 299;

		// Token: 0x04001CCD RID: 7373
		public const int ID_mhtmltag = 300;

		// Token: 0x04001CCE RID: 7374
		public const int ID_pmmetafile = 301;

		// Token: 0x04001CCF RID: 7375
		public const int ID_impr = 302;

		// Token: 0x04001CD0 RID: 7376
		public const int ID_pca = 303;

		// Token: 0x04001CD1 RID: 7377
		public const int ID_pnirohad = 304;

		// Token: 0x04001CD2 RID: 7378
		public const int ID_cs = 305;

		// Token: 0x04001CD3 RID: 7379
		public const int ID_fldinst = 306;

		// Token: 0x04001CD4 RID: 7380
		public const int ID_ab = 307;

		// Token: 0x04001CD5 RID: 7381
		public const int ID_fswiss = 308;

		// Token: 0x04001CD6 RID: 7382
		public const int ID_green = 309;

		// Token: 0x04001CD7 RID: 7383
		public const int ID_ulthd = 310;

		// Token: 0x04001CD8 RID: 7384
		public const int ID_ulc = 311;

		// Token: 0x04001CD9 RID: 7385
		public const int ID_af = 312;

		// Token: 0x04001CDA RID: 7386
		public const int ID_uld = 313;

		// Token: 0x04001CDB RID: 7387
		public const int ID_bgcross = 314;

		// Token: 0x04001CDC RID: 7388
		public const int ID_stylesheet = 315;

		// Token: 0x04001CDD RID: 7389
		public const int ID_listoverridetable = 316;

		// Token: 0x04001CDE RID: 7390
		public const int ID_background = 317;

		// Token: 0x04001CDF RID: 7391
		public const int ID_clcbpat = 318;

		// Token: 0x04001CE0 RID: 7392
		public const int ID_pntext = 319;

		// Token: 0x04001CE1 RID: 7393
		public const int ID_trftsWidth = 320;

		// Token: 0x04001CE2 RID: 7394
		public const int ID_brdrhair = 321;

		// Token: 0x04001CE3 RID: 7395
		public const int ID_pnlvlbody = 322;

		// Token: 0x04001CE4 RID: 7396
		public const int ID_super = 323;

		// Token: 0x04001CE5 RID: 7397
		public const int ID_objdata = 324;

		// Token: 0x04001CE6 RID: 7398
		public const int ID_ansi = 325;

		// Token: 0x04001CE7 RID: 7399
		public const int ID_ulthdashdd = 326;

		// Token: 0x04001CE8 RID: 7400
		public const int ID_ulp = 327;

		// Token: 0x04001CE9 RID: 7401
		public const int ID_brdrdot = 328;

		// Token: 0x04001CEA RID: 7402
		public const int ID_fromtext = 329;

		// Token: 0x04001CEB RID: 7403
		public const int ID_brdremboss = 330;

		// Token: 0x04001CEC RID: 7404
		public const int ID_cf = 331;

		// Token: 0x04001CED RID: 7405
		public static short[] keywordHashTable = new short[]
		{
			4,
			0,
			6,
			0,
			0,
			8,
			0,
			0,
			10,
			11,
			0,
			0,
			12,
			0,
			0,
			0,
			13,
			0,
			14,
			17,
			20,
			23,
			25,
			26,
			29,
			32,
			34,
			0,
			37,
			38,
			42,
			44,
			0,
			46,
			47,
			49,
			52,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			0,
			0,
			0,
			0,
			64,
			67,
			0,
			0,
			68,
			69,
			70,
			0,
			71,
			0,
			0,
			0,
			0,
			0,
			0,
			72,
			0,
			74,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			85,
			0,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			92,
			93,
			94,
			95,
			0,
			97,
			100,
			102,
			103,
			0,
			105,
			107,
			108,
			109,
			0,
			112,
			0,
			113,
			0,
			114,
			115,
			117,
			0,
			119,
			122,
			123,
			125,
			128,
			0,
			0,
			129,
			130,
			132,
			134,
			137,
			0,
			140,
			141,
			145,
			147,
			149,
			0,
			0,
			0,
			151,
			0,
			152,
			0,
			155,
			0,
			156,
			157,
			158,
			160,
			162,
			163,
			0,
			164,
			165,
			167,
			168,
			169,
			170,
			171,
			174,
			0,
			177,
			178,
			0,
			179,
			180,
			181,
			0,
			182,
			183,
			0,
			184,
			186,
			0,
			187,
			191,
			0,
			0,
			192,
			0,
			194,
			0,
			196,
			199,
			201,
			0,
			204,
			205,
			206,
			0,
			208,
			209,
			210,
			212,
			0,
			213,
			214,
			216,
			218,
			0,
			0,
			220,
			0,
			222,
			226,
			228,
			230,
			232,
			234,
			235,
			0,
			236,
			0,
			0,
			238,
			0,
			240,
			243,
			0,
			0,
			244,
			0,
			247,
			249,
			250,
			251,
			252,
			0,
			253,
			0,
			0,
			0,
			254,
			256,
			0,
			0,
			258,
			259,
			0,
			260,
			263,
			0,
			0,
			0,
			264,
			0,
			265,
			0,
			0,
			0,
			0,
			266,
			268,
			0,
			271,
			272,
			273,
			0,
			0,
			275,
			276,
			0,
			277,
			0,
			280,
			0,
			282,
			0,
			284,
			285,
			286,
			288,
			289,
			290,
			0,
			0,
			291,
			295,
			0,
			296,
			297,
			298,
			300,
			301,
			302,
			0,
			305,
			307,
			309,
			311,
			0,
			312,
			313,
			0,
			314,
			316,
			317,
			319,
			320,
			321,
			324,
			325,
			0,
			326,
			327,
			328,
			329,
			330,
			0
		};

		// Token: 0x04001CEE RID: 7406
		public static RTFData.KeyDef[] keywords = new RTFData.KeyDef[]
		{
			new RTFData.KeyDef(-1, 0, '\0', 0, false, "null"),
			new RTFData.KeyDef(-1, 0, '\0', 0, true, "null"),
			new RTFData.KeyDef(-1, 0, '\0', 0, false, "null"),
			new RTFData.KeyDef(-1, 0, '\0', 0, false, "null"),
			new RTFData.KeyDef(0, 0, '\0', 0, false, "aul"),
			new RTFData.KeyDef(0, 2, '\0', -1, false, "ulw"),
			new RTFData.KeyDef(2, 0, '\0', 0, false, "pichgoal"),
			new RTFData.KeyDef(2, 7, '\0', 0, false, "trbrdrb"),
			new RTFData.KeyDef(5, 0, '\0', 0, false, "leveltext"),
			new RTFData.KeyDef(5, 0, '\0', 0, false, "listlevel"),
			new RTFData.KeyDef(8, 8, '\0', 0, false, "trbrdrh"),
			new RTFData.KeyDef(9, 5, '\0', 0, false, "brdrengrave"),
			new RTFData.KeyDef(12, 4, '\0', 0, false, "trbrdrl"),
			new RTFData.KeyDef(16, 0, '\0', 0, false, "irow"),
			new RTFData.KeyDef(18, 4, '\0', 0, false, "brdrtriple"),
			new RTFData.KeyDef(18, 0, '\0', 0, false, "footer"),
			new RTFData.KeyDef(18, 6, '\0', 0, false, "trbrdrr"),
			new RTFData.KeyDef(19, 0, '\0', -1, false, "caps"),
			new RTFData.KeyDef(19, 4, '\0', 0, true, "fscript"),
			new RTFData.KeyDef(19, 5, '\0', -1, false, "uldash"),
			new RTFData.KeyDef(20, 0, '\0', 0, false, "expndtw"),
			new RTFData.KeyDef(20, 6, '\0', 0, false, "pnucrm"),
			new RTFData.KeyDef(20, 5, '\0', 0, false, "trbrdrt"),
			new RTFData.KeyDef(21, 4, '\0', 0, false, "brdrwavydb"),
			new RTFData.KeyDef(21, 0, '\0', 0, false, "header"),
			new RTFData.KeyDef(22, 9, '\0', 0, false, "trbrdrv"),
			new RTFData.KeyDef(23, 0, '\0', -1, false, "embo"),
			new RTFData.KeyDef(23, 0, '\0', 0, false, "pnindent"),
			new RTFData.KeyDef(23, 0, '‍', 0, false, "zwj"),
			new RTFData.KeyDef(24, 0, '\0', 0, false, "field"),
			new RTFData.KeyDef(24, 0, '\0', 0, true, "fnil"),
			new RTFData.KeyDef(24, 0, '\0', -1, false, "link"),
			new RTFData.KeyDef(25, 0, '\0', -1, false, "disabled"),
			new RTFData.KeyDef(25, 0, '\0', 0, false, "footnote"),
			new RTFData.KeyDef(26, 0, '\0', 0, true, "fcharset"),
			new RTFData.KeyDef(26, 0, '\0', 0, true, "mac"),
			new RTFData.KeyDef(26, 4, '\0', 0, false, "pnucltr"),
			new RTFData.KeyDef(28, 0, '\0', 0, true, "fbidis"),
			new RTFData.KeyDef(29, 0, '‘', 0, false, "lquote"),
			new RTFData.KeyDef(29, 0, '\0', 0, false, "macpict"),
			new RTFData.KeyDef(29, 0, '\0', 0, false, "row"),
			new RTFData.KeyDef(29, 1, '\0', 0, false, "rtlrow"),
			new RTFData.KeyDef(30, 0, '\0', 0, true, "fprq"),
			new RTFData.KeyDef(30, 0, '\0', 0, false, "picprop"),
			new RTFData.KeyDef(31, 0, '\0', 0, false, "levelstartat"),
			new RTFData.KeyDef(31, 0, '\0', 0, false, "pich"),
			new RTFData.KeyDef(33, 3, '\0', 0, false, "brdrwavy"),
			new RTFData.KeyDef(34, 0, '\0', 0, true, "bin"),
			new RTFData.KeyDef(34, 0, '\0', 0, false, "line"),
			new RTFData.KeyDef(35, 3, '\0', 0, true, "fmodern"),
			new RTFData.KeyDef(35, 0, '\0', 0, false, "pict"),
			new RTFData.KeyDef(35, 1, '\0', 0, false, "pnlvlblt"),
			new RTFData.KeyDef(36, 2, '\0', 0, false, "brdrdashsm"),
			new RTFData.KeyDef(36, 0, '\0', 0, false, "clcfpat"),
			new RTFData.KeyDef(36, 0, '\0', 0, false, "list"),
			new RTFData.KeyDef(36, 0, '\0', 0, false, "nestrow"),
			new RTFData.KeyDef(37, 0, '\0', 0, false, "brdrbtw"),
			new RTFData.KeyDef(38, 0, '\0', 0, false, "picw"),
			new RTFData.KeyDef(39, 0, '\0', 0, false, "cbpat"),
			new RTFData.KeyDef(40, 0, '‎', 0, false, "rtlmark"),
			new RTFData.KeyDef(41, 0, '\0', 0, true, "deflang"),
			new RTFData.KeyDef(42, 12, '\0', -1, false, "ulhwave"),
			new RTFData.KeyDef(43, 0, '\0', 0, false, "pnaiud"),
			new RTFData.KeyDef(44, 13, '\0', -1, false, "ulldash"),
			new RTFData.KeyDef(49, 1, '\0', 0, false, "brdrdashdotstr"),
			new RTFData.KeyDef(49, 0, '\0', 0, false, "nesttableprops"),
			new RTFData.KeyDef(49, 0, '\0', 0, false, "trleft"),
			new RTFData.KeyDef(50, 2, '\0', 0, false, "bghoriz"),
			new RTFData.KeyDef(53, 0, '\0', 0, false, "par"),
			new RTFData.KeyDef(54, 0, '\0', 0, false, "keepn"),
			new RTFData.KeyDef(55, 0, '\0', 0, false, "pnordt"),
			new RTFData.KeyDef(57, 0, '\0', 0, true, "lang"),
			new RTFData.KeyDef(64, 6, '\0', 0, true, "fbidi"),
			new RTFData.KeyDef(64, 0, '\0', 0, false, "lastrow"),
			new RTFData.KeyDef(66, 0, '•', 0, false, "bullet"),
			new RTFData.KeyDef(66, 0, '\0', 0, false, "sectd"),
			new RTFData.KeyDef(68, 1, '\0', -1, false, "ul"),
			new RTFData.KeyDef(69, 3, '\0', 0, false, "pnlcltr"),
			new RTFData.KeyDef(70, 0, '\0', 0, false, "clvmrg"),
			new RTFData.KeyDef(71, 0, '\0', -1, false, "shad"),
			new RTFData.KeyDef(72, 2, '\0', 0, false, "brdrdash"),
			new RTFData.KeyDef(73, 0, '\0', 1, true, "uc"),
			new RTFData.KeyDef(74, 0, '\0', 0, false, "highlight"),
			new RTFData.KeyDef(75, 0, '\0', 0, false, "htmlbase"),
			new RTFData.KeyDef(75, 0, '\0', 0, false, "pncnum"),
			new RTFData.KeyDef(76, 0, '\0', 0, false, "ud"),
			new RTFData.KeyDef(80, 0, '\0', 0, false, "pnstart"),
			new RTFData.KeyDef(81, 0, '\0', 0, true, "adeff"),
			new RTFData.KeyDef(84, 0, '\0', 0, false, "blue"),
			new RTFData.KeyDef(84, 4, '\0', 0, false, "brdrdb"),
			new RTFData.KeyDef(84, 4, '\0', 0, false, "brdrframe"),
			new RTFData.KeyDef(84, 0, '\0', 0, false, "taprtl"),
			new RTFData.KeyDef(85, 0, '\0', 0, false, "comment"),
			new RTFData.KeyDef(86, 1, '\0', 0, true, "froman"),
			new RTFData.KeyDef(87, 5, '\0', 0, true, "fdecor"),
			new RTFData.KeyDef(88, 4, '\0', 0, true, "dbch"),
			new RTFData.KeyDef(88, 1, '\0', 0, false, "up"),
			new RTFData.KeyDef(90, 8, '\0', 0, false, "brdroutset"),
			new RTFData.KeyDef(90, 2, '\0', 0, false, "clvertalb"),
			new RTFData.KeyDef(90, 0, '\0', -1, false, "striked"),
			new RTFData.KeyDef(91, 1, '\0', 0, false, "clvertalc"),
			new RTFData.KeyDef(91, 0, '\0', 1, false, "itap"),
			new RTFData.KeyDef(92, 0, '\0', 0, false, "pnlvl"),
			new RTFData.KeyDef(93, 0, '\0', 0, false, "clftsWidth"),
			new RTFData.KeyDef(93, 0, '\0', 0, false, "upr"),
			new RTFData.KeyDef(95, 0, '\0', 0, false, "nestcell"),
			new RTFData.KeyDef(95, 0, '\0', 0, false, "pnord"),
			new RTFData.KeyDef(96, 0, '\0', -1, false, "protect"),
			new RTFData.KeyDef(97, 0, '\0', 0, true, "pc"),
			new RTFData.KeyDef(98, 0, '\0', -1, true, "b"),
			new RTFData.KeyDef(98, 0, '\0', 0, false, "deftab"),
			new RTFData.KeyDef(98, 3, '\0', 0, false, "qj"),
			new RTFData.KeyDef(100, 0, '\0', 0, false, "ql"),
			new RTFData.KeyDef(102, 0, '\0', -1, true, "f"),
			new RTFData.KeyDef(104, 0, '\0', 0, false, "sp"),
			new RTFData.KeyDef(105, 0, '\0', -1, true, "i"),
			new RTFData.KeyDef(105, 1, '\0', 0, false, "qc"),
			new RTFData.KeyDef(106, 0, '\0', -1, false, "revised"),
			new RTFData.KeyDef(106, 0, '\0', 0, false, "trpaddr"),
			new RTFData.KeyDef(108, 0, '\0', 0, false, "cell"),
			new RTFData.KeyDef(108, 4, '\0', 0, false, "qd"),
			new RTFData.KeyDef(108, 0, '\0', 0, false, "trpaddt"),
			new RTFData.KeyDef(109, 4, '\0', 0, false, "brdrthtnlg"),
			new RTFData.KeyDef(110, 0, '\0', 0, false, "pn"),
			new RTFData.KeyDef(110, 0, '\0', 0, false, "sv"),
			new RTFData.KeyDef(111, 0, '\0', 0, false, "brsp"),
			new RTFData.KeyDef(111, 0, '\0', 0, false, "tab"),
			new RTFData.KeyDef(111, 0, '\0', 0, false, "trgaph"),
			new RTFData.KeyDef(112, 0, '\0', 0, false, "shp"),
			new RTFData.KeyDef(115, 0, '\0', 0, false, "s"),
			new RTFData.KeyDef(116, 0, '\0', 0, false, "sl"),
			new RTFData.KeyDef(116, 0, '\0', 0, false, "trpaddl"),
			new RTFData.KeyDef(117, 4, '\0', 0, false, "brdrthtnmg"),
			new RTFData.KeyDef(117, 0, '\0', 0, true, "u"),
			new RTFData.KeyDef(118, 0, '\0', 0, true, "ltrch"),
			new RTFData.KeyDef(118, 0, '\0', 0, false, "sn"),
			new RTFData.KeyDef(118, 0, '\0', -1, false, "v"),
			new RTFData.KeyDef(119, 3, '\0', 0, true, "hich"),
			new RTFData.KeyDef(119, 0, '\0', 0, false, "ri"),
			new RTFData.KeyDef(119, 0, '\0', 0, false, "sa"),
			new RTFData.KeyDef(121, 5, '\0', 0, false, "qs"),
			new RTFData.KeyDef(122, 2, '\0', 0, false, "qr"),
			new RTFData.KeyDef(122, 0, '\0', 0, false, "sb"),
			new RTFData.KeyDef(122, 0, '\0', 0, false, "trcbpat"),
			new RTFData.KeyDef(122, 0, '\0', 0, false, "trpaddb"),
			new RTFData.KeyDef(123, 0, '\0', 0, false, "cfpat"),
			new RTFData.KeyDef(123, 0, '\0', 0, false, "keep"),
			new RTFData.KeyDef(124, 1, '\0', 0, false, "bgvert"),
			new RTFData.KeyDef(124, 2, '\0', 0, false, "red"),
			new RTFData.KeyDef(125, 0, '\0', 0, true, "deflangfe"),
			new RTFData.KeyDef(125, 11, '\0', -1, false, "ululdbwave"),
			new RTFData.KeyDef(129, 0, '\0', 0, false, "trrh"),
			new RTFData.KeyDef(131, 0, '\0', 0, false, "_hyphen"),
			new RTFData.KeyDef(131, 0, '\0', 1, false, "htmlrtf"),
			new RTFData.KeyDef(131, 0, '\0', 0, false, "picwgoal"),
			new RTFData.KeyDef(133, 7, '\0', -1, false, "uldashdd"),
			new RTFData.KeyDef(135, 4, '\0', 0, false, "brdrtnthsg"),
			new RTFData.KeyDef(136, 0, '\0', 0, false, "objattph"),
			new RTFData.KeyDef(137, 9, '\0', 0, false, "bgdkbdiag"),
			new RTFData.KeyDef(137, 3, '\0', -1, false, "uldb"),
			new RTFData.KeyDef(138, 0, '\0', 0, false, "clmrg"),
			new RTFData.KeyDef(138, 0, '\0', 0, false, "clpadr"),
			new RTFData.KeyDef(139, 0, '\0', -1, false, "outl"),
			new RTFData.KeyDef(140, 0, '\0', 0, false, "clpadt"),
			new RTFData.KeyDef(142, 0, '\0', 0, true, "fcs"),
			new RTFData.KeyDef(143, 0, '\0', 0, true, "ansicpg"),
			new RTFData.KeyDef(143, 0, '\0', 0, false, "shpinst"),
			new RTFData.KeyDef(144, 0, '\0', 0, false, "brdrcf"),
			new RTFData.KeyDef(145, 0, '\0', 0, false, "sect"),
			new RTFData.KeyDef(146, 0, '\0', 0, true, "afs"),
			new RTFData.KeyDef(147, 0, '\0', 0, true, "plain"),
			new RTFData.KeyDef(148, 7, '\0', 0, false, "brdrinset"),
			new RTFData.KeyDef(148, 0, '\0', 0, false, "clpadl"),
			new RTFData.KeyDef(148, 0, '\0', 0, false, "listid"),
			new RTFData.KeyDef(149, 0, '\0', 0, false, "acf"),
			new RTFData.KeyDef(149, 0, '\0', 0, true, "fonttbl"),
			new RTFData.KeyDef(149, 1, '\0', 0, false, "rtlpar"),
			new RTFData.KeyDef(151, 0, '\0', 0, false, "htmltag"),
			new RTFData.KeyDef(152, 15, '\0', -1, false, "ulthdashd"),
			new RTFData.KeyDef(154, 0, '\0', 0, false, "clpadb"),
			new RTFData.KeyDef(155, 0, '\0', -1, false, "scaps"),
			new RTFData.KeyDef(156, 12, '\0', 0, false, "clbrdrr"),
			new RTFData.KeyDef(158, 0, '\0', 0, false, "shading"),
			new RTFData.KeyDef(159, 2, '\0', 0, false, "trqr"),
			new RTFData.KeyDef(161, 0, '\0', 0, false, "pard"),
			new RTFData.KeyDef(161, 0, '\0', 0, true, "pnfs"),
			new RTFData.KeyDef(162, 11, '\0', 0, false, "clbrdrt"),
			new RTFData.KeyDef(164, 4, '\0', 0, false, "bgdkvert"),
			new RTFData.KeyDef(164, 0, '\0', 0, false, "brdrl"),
			new RTFData.KeyDef(164, 0, '‏', 0, false, "ltrmark"),
			new RTFData.KeyDef(164, 18, '\0', -1, false, "ulthldash"),
			new RTFData.KeyDef(165, 0, '\0', 0, false, "intbl"),
			new RTFData.KeyDef(168, 12, '\0', 0, false, "bgbdiag"),
			new RTFData.KeyDef(168, 0, '\0', 0, false, "bkmkstart"),
			new RTFData.KeyDef(170, 3, '\0', 0, false, "brdrb"),
			new RTFData.KeyDef(170, 10, '\0', 0, false, "clbrdrl"),
			new RTFData.KeyDef(172, 13, '\0', 0, false, "clbrdrb"),
			new RTFData.KeyDef(172, 0, '\0', 0, false, "pagebb"),
			new RTFData.KeyDef(172, 0, '\0', -1, false, "strike"),
			new RTFData.KeyDef(173, 0, '\0', 0, false, "clvmgf"),
			new RTFData.KeyDef(173, 0, '\0', 0, false, "trowd"),
			new RTFData.KeyDef(174, 0, '\0', 0, false, "info"),
			new RTFData.KeyDef(174, 0, '“', 0, false, "ldblquote"),
			new RTFData.KeyDef(174, 0, '\0', 0, false, "listoverride"),
			new RTFData.KeyDef(176, 1, '\0', 0, false, "trqc"),
			new RTFData.KeyDef(177, 0, '\0', 0, false, "ilvl"),
			new RTFData.KeyDef(178, 0, '\0', 0, false, "li"),
			new RTFData.KeyDef(178, 0, '‌', 0, false, "zwnj"),
			new RTFData.KeyDef(180, 0, '\0', 0, false, "listsimple"),
			new RTFData.KeyDef(181, 2, '\0', 0, false, "brdrdashdd"),
			new RTFData.KeyDef(182, 0, '\0', 0, true, "fname"),
			new RTFData.KeyDef(182, 0, '\0', 0, false, "ulnone"),
			new RTFData.KeyDef(183, 0, '\0', 0, false, "bkmkend"),
			new RTFData.KeyDef(185, 0, '\0', 0, false, "clwWidth"),
			new RTFData.KeyDef(186, 0, '\0', 0, true, "adeflang"),
			new RTFData.KeyDef(186, 2, '\0', 0, false, "brdrr"),
			new RTFData.KeyDef(187, 3, '\0', 0, false, "brdrs"),
			new RTFData.KeyDef(187, 0, '\0', 0, false, "pniroha"),
			new RTFData.KeyDef(188, 1, '\0', 0, false, "brdrt"),
			new RTFData.KeyDef(188, 0, '\0', 0, false, "ls"),
			new RTFData.KeyDef(191, 0, '\0', 0, false, "brdrw"),
			new RTFData.KeyDef(191, 0, '\0', 0, false, "irowband"),
			new RTFData.KeyDef(193, 2, '\0', 0, true, "loch"),
			new RTFData.KeyDef(193, 0, '\0', -1, true, "pnf"),
			new RTFData.KeyDef(193, 0, '\0', 0, false, "slmult"),
			new RTFData.KeyDef(193, 14, '\0', -1, false, "ulthdash"),
			new RTFData.KeyDef(194, 7, '\0', 0, false, "bgdkdcross"),
			new RTFData.KeyDef(194, 0, '\0', 0, false, "pntxta"),
			new RTFData.KeyDef(195, 0, '\0', 0, false, "cellx"),
			new RTFData.KeyDef(195, 0, '\0', 0, false, "pnlvlcont"),
			new RTFData.KeyDef(196, 0, '\0', 0, false, "headerl"),
			new RTFData.KeyDef(196, 0, '\0', 0, false, "jclisttab"),
			new RTFData.KeyDef(197, 0, '–', 0, false, "endash"),
			new RTFData.KeyDef(197, 0, '\0', 0, false, "pntxtb"),
			new RTFData.KeyDef(198, 3, '\0', 0, false, "bgfdiag"),
			new RTFData.KeyDef(199, 0, '\0', 0, false, "brdrbar"),
			new RTFData.KeyDef(201, 6, '\0', 0, false, "bgdkfdiag"),
			new RTFData.KeyDef(201, 0, '\0', 0, false, "trwWidth"),
			new RTFData.KeyDef(204, 0, '\0', 0, true, "alang"),
			new RTFData.KeyDef(204, 4, '\0', 0, false, "brdrtnthtnlg"),
			new RTFData.KeyDef(206, 0, '\0', 0, true, "cpg"),
			new RTFData.KeyDef(206, 0, '\0', 0, false, "headerf"),
			new RTFData.KeyDef(206, 0, '\0', 0, false, "ltrrow"),
			new RTFData.KeyDef(207, 4, '\0', 0, false, "brdrtnthtnsg"),
			new RTFData.KeyDef(210, 5, '\0', 0, false, "bgdkhoriz"),
			new RTFData.KeyDef(210, 0, '\0', 0, false, "dropcapt"),
			new RTFData.KeyDef(210, 0, '\0', 0, false, "listtable"),
			new RTFData.KeyDef(212, 4, '\0', 0, false, "brdrtnthtnmg"),
			new RTFData.KeyDef(212, 5, '\0', 0, false, "pnlcrm"),
			new RTFData.KeyDef(213, 0, '\0', 0, false, "pndecd"),
			new RTFData.KeyDef(214, 0, '\0', 0, false, "leveljc"),
			new RTFData.KeyDef(215, 4, '\0', 0, false, "brdrtnthmg"),
			new RTFData.KeyDef(216, 0, '\0', 0, false, "colortbl"),
			new RTFData.KeyDef(218, 0, '\0', 0, false, "headerr"),
			new RTFData.KeyDef(222, 3, '\0', 0, false, "brdrth"),
			new RTFData.KeyDef(222, 0, '\0', 0, false, "levelpicture"),
			new RTFData.KeyDef(223, 4, '\0', 0, false, "brdrtnthlg"),
			new RTFData.KeyDef(223, 0, '\0', 0, false, "listtext"),
			new RTFData.KeyDef(226, 0, '\0', 0, false, "footerr"),
			new RTFData.KeyDef(227, 0, '\0', 0, false, "clmgf"),
			new RTFData.KeyDef(229, 10, '\0', 0, false, "bgdcross"),
			new RTFData.KeyDef(229, 4, '\0', 0, false, "brdrthtnsg"),
			new RTFData.KeyDef(229, 0, '\0', -1, false, "sub"),
			new RTFData.KeyDef(230, 0, '”', 0, false, "rdblquote"),
			new RTFData.KeyDef(234, 0, '\0', 0, false, "nosupersub"),
			new RTFData.KeyDef(236, 0, '\0', 0, true, "fs"),
			new RTFData.KeyDef(241, 7, '\0', 0, true, "ftech"),
			new RTFData.KeyDef(241, 0, '\0', 0, true, "rtf"),
			new RTFData.KeyDef(242, 0, '\0', 0, true, "falt"),
			new RTFData.KeyDef(242, 0, '\0', 0, false, "fldrslt"),
			new RTFData.KeyDef(242, 0, '\0', 0, false, "ltrpar"),
			new RTFData.KeyDef(244, 0, '\0', 0, true, "urtf"),
			new RTFData.KeyDef(245, 0, '\0', 0, false, "pncard"),
			new RTFData.KeyDef(246, 0, '\0', 0, false, "footerf"),
			new RTFData.KeyDef(246, 2, '\0', 0, false, "pndec"),
			new RTFData.KeyDef(249, -1, '\0', 0, false, "dn"),
			new RTFData.KeyDef(250, 2, '\0', 0, false, "brdrdashd"),
			new RTFData.KeyDef(252, 0, '\0', 0, false, "footerl"),
			new RTFData.KeyDef(252, 6, '\0', -1, false, "uldashd"),
			new RTFData.KeyDef(252, 8, '\0', -1, false, "ulwave"),
			new RTFData.KeyDef(254, 0, '\0', 0, true, "deff"),
			new RTFData.KeyDef(254, 0, '\0', 0, true, "langfe"),
			new RTFData.KeyDef(256, 8, '\0', 0, false, "bgdkcross"),
			new RTFData.KeyDef(256, 0, '\0', 0, false, "nonesttables"),
			new RTFData.KeyDef(258, 0, '\0', 0, false, "fi"),
			new RTFData.KeyDef(259, 0, '\0', 0, false, "chcbpat"),
			new RTFData.KeyDef(260, 0, '’', 0, false, "rquote"),
			new RTFData.KeyDef(260, 1, '\0', 0, true, "rtlch"),
			new RTFData.KeyDef(261, 0, '\0', 0, false, "pndbnum"),
			new RTFData.KeyDef(262, 3, '\0', 0, false, "brdrsh"),
			new RTFData.KeyDef(263, 0, '\0', 0, false, "pnaiu"),
			new RTFData.KeyDef(266, 0, '\0', -1, true, "ai"),
			new RTFData.KeyDef(266, 0, '\0', 0, true, "fromhtml"),
			new RTFData.KeyDef(266, 0, '\0', 0, false, "trhdr"),
			new RTFData.KeyDef(266, 10, '\0', -1, false, "ulhair"),
			new RTFData.KeyDef(267, 0, '—', 0, false, "emdash"),
			new RTFData.KeyDef(269, 0, '\0', 0, false, "levelnfc"),
			new RTFData.KeyDef(270, 0, '\0', -1, false, "deleted"),
			new RTFData.KeyDef(271, 0, '\0', 0, false, "dropcapli"),
			new RTFData.KeyDef(271, 9, '\0', -1, false, "ulth"),
			new RTFData.KeyDef(272, 0, '\0', 0, false, "mhtmltag"),
			new RTFData.KeyDef(273, 0, '\0', 0, false, "pmmetafile"),
			new RTFData.KeyDef(274, 0, '\0', -1, false, "impr"),
			new RTFData.KeyDef(274, 0, '\0', 0, true, "pca"),
			new RTFData.KeyDef(274, 0, '\0', 0, false, "pnirohad"),
			new RTFData.KeyDef(276, 0, '\0', 0, false, "cs"),
			new RTFData.KeyDef(276, 0, '\0', 0, false, "fldinst"),
			new RTFData.KeyDef(277, 0, '\0', -1, true, "ab"),
			new RTFData.KeyDef(277, 2, '\0', 0, true, "fswiss"),
			new RTFData.KeyDef(278, 1, '\0', 0, false, "green"),
			new RTFData.KeyDef(278, 17, '\0', -1, false, "ulthd"),
			new RTFData.KeyDef(279, 0, '\0', 0, false, "ulc"),
			new RTFData.KeyDef(281, 0, '\0', -1, true, "af"),
			new RTFData.KeyDef(282, 4, '\0', -1, false, "uld"),
			new RTFData.KeyDef(284, 11, '\0', 0, false, "bgcross"),
			new RTFData.KeyDef(284, 0, '\0', 0, false, "stylesheet"),
			new RTFData.KeyDef(285, 0, '\0', 0, false, "listoverridetable"),
			new RTFData.KeyDef(286, 0, '\0', 0, false, "background"),
			new RTFData.KeyDef(286, 0, '\0', 0, false, "clcbpat"),
			new RTFData.KeyDef(287, 0, '\0', 0, false, "pntext"),
			new RTFData.KeyDef(288, 0, '\0', 0, false, "trftsWidth"),
			new RTFData.KeyDef(289, 3, '\0', 0, false, "brdrhair"),
			new RTFData.KeyDef(289, 0, '\0', 0, false, "pnlvlbody"),
			new RTFData.KeyDef(289, 0, '\0', -1, false, "super"),
			new RTFData.KeyDef(290, 0, '\0', 0, false, "objdata"),
			new RTFData.KeyDef(291, 0, '\0', 0, true, "ansi"),
			new RTFData.KeyDef(293, 16, '\0', -1, false, "ulthdashdd"),
			new RTFData.KeyDef(294, 0, '\0', 0, false, "ulp"),
			new RTFData.KeyDef(295, 1, '\0', 0, false, "brdrdot"),
			new RTFData.KeyDef(296, 0, '\0', 0, true, "fromtext"),
			new RTFData.KeyDef(297, 6, '\0', 0, false, "brdremboss"),
			new RTFData.KeyDef(297, 0, '\0', 0, false, "cf")
		};

		// Token: 0x04001CEF RID: 7407
		public static int[] allowedCodePages = new int[]
		{
			28591,
			437,
			708,
			720,
			850,
			852,
			860,
			862,
			863,
			864,
			865,
			866,
			874,
			932,
			936,
			949,
			950,
			1250,
			1251,
			1252,
			1253,
			1254,
			1255,
			1256,
			1257,
			1258,
			1361,
			10001,
			10002,
			10003,
			10008,
			65001
		};

		// Token: 0x02000251 RID: 593
		public struct KeyDef
		{
			// Token: 0x0600187E RID: 6270 RVA: 0x000C4531 File Offset: 0x000C2731
			public KeyDef(short hash, short idx, char character, short defaultValue, bool affectsParsing, string name)
			{
				this.hash = hash;
				this.idx = idx;
				this.character = character;
				this.defaultValue = defaultValue;
				this.affectsParsing = affectsParsing;
				this.name = name;
			}

			// Token: 0x04001CF0 RID: 7408
			public short hash;

			// Token: 0x04001CF1 RID: 7409
			public short idx;

			// Token: 0x04001CF2 RID: 7410
			public char character;

			// Token: 0x04001CF3 RID: 7411
			public short defaultValue;

			// Token: 0x04001CF4 RID: 7412
			public bool affectsParsing;

			// Token: 0x04001CF5 RID: 7413
			public string name;
		}
	}
}
