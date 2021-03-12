using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E8 RID: 488
	internal enum HtmlTagIndex : byte
	{
		// Token: 0x040014BD RID: 5309
		_NULL,
		// Token: 0x040014BE RID: 5310
		_ROOT,
		// Token: 0x040014BF RID: 5311
		_COMMENT,
		// Token: 0x040014C0 RID: 5312
		_CONDITIONAL,
		// Token: 0x040014C1 RID: 5313
		_BANG,
		// Token: 0x040014C2 RID: 5314
		_DTD,
		// Token: 0x040014C3 RID: 5315
		_ASP,
		// Token: 0x040014C4 RID: 5316
		Unknown,
		// Token: 0x040014C5 RID: 5317
		A,
		// Token: 0x040014C6 RID: 5318
		Abbr,
		// Token: 0x040014C7 RID: 5319
		Acronym,
		// Token: 0x040014C8 RID: 5320
		Address,
		// Token: 0x040014C9 RID: 5321
		Applet,
		// Token: 0x040014CA RID: 5322
		Area,
		// Token: 0x040014CB RID: 5323
		B,
		// Token: 0x040014CC RID: 5324
		Base,
		// Token: 0x040014CD RID: 5325
		BaseFont,
		// Token: 0x040014CE RID: 5326
		Bdo,
		// Token: 0x040014CF RID: 5327
		BGSound,
		// Token: 0x040014D0 RID: 5328
		Big,
		// Token: 0x040014D1 RID: 5329
		Blink,
		// Token: 0x040014D2 RID: 5330
		BlockQuote,
		// Token: 0x040014D3 RID: 5331
		Body,
		// Token: 0x040014D4 RID: 5332
		BR,
		// Token: 0x040014D5 RID: 5333
		Button,
		// Token: 0x040014D6 RID: 5334
		Caption,
		// Token: 0x040014D7 RID: 5335
		Center,
		// Token: 0x040014D8 RID: 5336
		Cite,
		// Token: 0x040014D9 RID: 5337
		Code,
		// Token: 0x040014DA RID: 5338
		Col,
		// Token: 0x040014DB RID: 5339
		ColGroup,
		// Token: 0x040014DC RID: 5340
		Comment,
		// Token: 0x040014DD RID: 5341
		DD,
		// Token: 0x040014DE RID: 5342
		Del,
		// Token: 0x040014DF RID: 5343
		Dfn,
		// Token: 0x040014E0 RID: 5344
		Dir,
		// Token: 0x040014E1 RID: 5345
		Div,
		// Token: 0x040014E2 RID: 5346
		DL,
		// Token: 0x040014E3 RID: 5347
		DT,
		// Token: 0x040014E4 RID: 5348
		EM,
		// Token: 0x040014E5 RID: 5349
		Embed,
		// Token: 0x040014E6 RID: 5350
		FieldSet,
		// Token: 0x040014E7 RID: 5351
		Font,
		// Token: 0x040014E8 RID: 5352
		Form,
		// Token: 0x040014E9 RID: 5353
		Frame,
		// Token: 0x040014EA RID: 5354
		FrameSet,
		// Token: 0x040014EB RID: 5355
		H1,
		// Token: 0x040014EC RID: 5356
		H2,
		// Token: 0x040014ED RID: 5357
		H3,
		// Token: 0x040014EE RID: 5358
		H4,
		// Token: 0x040014EF RID: 5359
		H5,
		// Token: 0x040014F0 RID: 5360
		H6,
		// Token: 0x040014F1 RID: 5361
		Head,
		// Token: 0x040014F2 RID: 5362
		HR,
		// Token: 0x040014F3 RID: 5363
		Html,
		// Token: 0x040014F4 RID: 5364
		I,
		// Token: 0x040014F5 RID: 5365
		Iframe,
		// Token: 0x040014F6 RID: 5366
		Image,
		// Token: 0x040014F7 RID: 5367
		Img,
		// Token: 0x040014F8 RID: 5368
		Input,
		// Token: 0x040014F9 RID: 5369
		Ins,
		// Token: 0x040014FA RID: 5370
		IsIndex,
		// Token: 0x040014FB RID: 5371
		Kbd,
		// Token: 0x040014FC RID: 5372
		Label,
		// Token: 0x040014FD RID: 5373
		Legend,
		// Token: 0x040014FE RID: 5374
		LI,
		// Token: 0x040014FF RID: 5375
		Link,
		// Token: 0x04001500 RID: 5376
		Listing,
		// Token: 0x04001501 RID: 5377
		Map,
		// Token: 0x04001502 RID: 5378
		Marquee,
		// Token: 0x04001503 RID: 5379
		Menu,
		// Token: 0x04001504 RID: 5380
		Meta,
		// Token: 0x04001505 RID: 5381
		NextId,
		// Token: 0x04001506 RID: 5382
		NoBR,
		// Token: 0x04001507 RID: 5383
		NoEmbed,
		// Token: 0x04001508 RID: 5384
		NoFrames,
		// Token: 0x04001509 RID: 5385
		NoScript,
		// Token: 0x0400150A RID: 5386
		Object,
		// Token: 0x0400150B RID: 5387
		OL,
		// Token: 0x0400150C RID: 5388
		OptGroup,
		// Token: 0x0400150D RID: 5389
		Option,
		// Token: 0x0400150E RID: 5390
		P,
		// Token: 0x0400150F RID: 5391
		Param,
		// Token: 0x04001510 RID: 5392
		PlainText,
		// Token: 0x04001511 RID: 5393
		Pre,
		// Token: 0x04001512 RID: 5394
		Q,
		// Token: 0x04001513 RID: 5395
		RP,
		// Token: 0x04001514 RID: 5396
		RT,
		// Token: 0x04001515 RID: 5397
		Ruby,
		// Token: 0x04001516 RID: 5398
		S,
		// Token: 0x04001517 RID: 5399
		Samp,
		// Token: 0x04001518 RID: 5400
		Script,
		// Token: 0x04001519 RID: 5401
		Select,
		// Token: 0x0400151A RID: 5402
		Small,
		// Token: 0x0400151B RID: 5403
		Span,
		// Token: 0x0400151C RID: 5404
		Data,
		// Token: 0x0400151D RID: 5405
		Meter,
		// Token: 0x0400151E RID: 5406
		Strike,
		// Token: 0x0400151F RID: 5407
		Strong,
		// Token: 0x04001520 RID: 5408
		Style,
		// Token: 0x04001521 RID: 5409
		Sub,
		// Token: 0x04001522 RID: 5410
		Sup,
		// Token: 0x04001523 RID: 5411
		Table,
		// Token: 0x04001524 RID: 5412
		Tbody,
		// Token: 0x04001525 RID: 5413
		TC,
		// Token: 0x04001526 RID: 5414
		TD,
		// Token: 0x04001527 RID: 5415
		TextArea,
		// Token: 0x04001528 RID: 5416
		Tfoot,
		// Token: 0x04001529 RID: 5417
		TH,
		// Token: 0x0400152A RID: 5418
		Thead,
		// Token: 0x0400152B RID: 5419
		Title,
		// Token: 0x0400152C RID: 5420
		TR,
		// Token: 0x0400152D RID: 5421
		TT,
		// Token: 0x0400152E RID: 5422
		U,
		// Token: 0x0400152F RID: 5423
		UL,
		// Token: 0x04001530 RID: 5424
		Var,
		// Token: 0x04001531 RID: 5425
		Wbr,
		// Token: 0x04001532 RID: 5426
		Xmp,
		// Token: 0x04001533 RID: 5427
		Xml,
		// Token: 0x04001534 RID: 5428
		_Pxml,
		// Token: 0x04001535 RID: 5429
		_Import,
		// Token: 0x04001536 RID: 5430
		_Xml_Namespace,
		// Token: 0x04001537 RID: 5431
		_IMPLICIT_BEGIN
	}
}
