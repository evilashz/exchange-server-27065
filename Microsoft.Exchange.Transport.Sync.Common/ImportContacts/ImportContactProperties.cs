using System;

namespace Microsoft.Exchange.Transport.Sync.Common.ImportContacts
{
	// Token: 0x02000073 RID: 115
	internal enum ImportContactProperties
	{
		// Token: 0x04000134 RID: 308
		IgnoredProperty,
		// Token: 0x04000135 RID: 309
		Title,
		// Token: 0x04000136 RID: 310
		FirstName,
		// Token: 0x04000137 RID: 311
		MiddleName,
		// Token: 0x04000138 RID: 312
		LastName,
		// Token: 0x04000139 RID: 313
		Suffix,
		// Token: 0x0400013A RID: 314
		JobTitle,
		// Token: 0x0400013B RID: 315
		Nickname,
		// Token: 0x0400013C RID: 316
		Company,
		// Token: 0x0400013D RID: 317
		Department,
		// Token: 0x0400013E RID: 318
		BusinessStreet,
		// Token: 0x0400013F RID: 319
		BusinessStreet2,
		// Token: 0x04000140 RID: 320
		BusinessStreet3,
		// Token: 0x04000141 RID: 321
		BusinessCity,
		// Token: 0x04000142 RID: 322
		BusinessState,
		// Token: 0x04000143 RID: 323
		BusinessPostalCode,
		// Token: 0x04000144 RID: 324
		BusinessCountryOrRegion,
		// Token: 0x04000145 RID: 325
		BusinessPOBox,
		// Token: 0x04000146 RID: 326
		HomeStreet,
		// Token: 0x04000147 RID: 327
		HomeStreet2,
		// Token: 0x04000148 RID: 328
		HomeStreet3,
		// Token: 0x04000149 RID: 329
		HomeCity,
		// Token: 0x0400014A RID: 330
		HomeState,
		// Token: 0x0400014B RID: 331
		HomePostalCode,
		// Token: 0x0400014C RID: 332
		HomeCountryOrRegion,
		// Token: 0x0400014D RID: 333
		HomePOBox,
		// Token: 0x0400014E RID: 334
		OtherStreet,
		// Token: 0x0400014F RID: 335
		OtherStreet2,
		// Token: 0x04000150 RID: 336
		OtherStreet3,
		// Token: 0x04000151 RID: 337
		OtherCity,
		// Token: 0x04000152 RID: 338
		OtherState,
		// Token: 0x04000153 RID: 339
		OtherPostalCode,
		// Token: 0x04000154 RID: 340
		OtherCountryOrRegion,
		// Token: 0x04000155 RID: 341
		OtherPOBox,
		// Token: 0x04000156 RID: 342
		AssistantPhone,
		// Token: 0x04000157 RID: 343
		BusinessFax,
		// Token: 0x04000158 RID: 344
		BusinessPhone,
		// Token: 0x04000159 RID: 345
		BusinessPhone2,
		// Token: 0x0400015A RID: 346
		Callback,
		// Token: 0x0400015B RID: 347
		CarPhone,
		// Token: 0x0400015C RID: 348
		CompanyMainPhone,
		// Token: 0x0400015D RID: 349
		HomeFax,
		// Token: 0x0400015E RID: 350
		HomePhone,
		// Token: 0x0400015F RID: 351
		HomePhone2,
		// Token: 0x04000160 RID: 352
		ISDN,
		// Token: 0x04000161 RID: 353
		MobilePhone,
		// Token: 0x04000162 RID: 354
		OtherFax,
		// Token: 0x04000163 RID: 355
		OtherPhone,
		// Token: 0x04000164 RID: 356
		Pager,
		// Token: 0x04000165 RID: 357
		PrimaryPhone,
		// Token: 0x04000166 RID: 358
		RadioPhone,
		// Token: 0x04000167 RID: 359
		TTYOrTDDPhone,
		// Token: 0x04000168 RID: 360
		Telex,
		// Token: 0x04000169 RID: 361
		Anniversary,
		// Token: 0x0400016A RID: 362
		AssistantName,
		// Token: 0x0400016B RID: 363
		Birthday,
		// Token: 0x0400016C RID: 364
		Categories,
		// Token: 0x0400016D RID: 365
		Children,
		// Token: 0x0400016E RID: 366
		EmailAddress,
		// Token: 0x0400016F RID: 367
		EmailType,
		// Token: 0x04000170 RID: 368
		EmailDisplayName,
		// Token: 0x04000171 RID: 369
		Email2Address,
		// Token: 0x04000172 RID: 370
		Email2Type,
		// Token: 0x04000173 RID: 371
		Email2DisplayName,
		// Token: 0x04000174 RID: 372
		Email3Address,
		// Token: 0x04000175 RID: 373
		Email3Type,
		// Token: 0x04000176 RID: 374
		Email3DisplayName,
		// Token: 0x04000177 RID: 375
		GovernmentIDNumber,
		// Token: 0x04000178 RID: 376
		Hobby,
		// Token: 0x04000179 RID: 377
		Initials,
		// Token: 0x0400017A RID: 378
		InternetFreeBusy,
		// Token: 0x0400017B RID: 379
		ManagerName,
		// Token: 0x0400017C RID: 380
		Mileage,
		// Token: 0x0400017D RID: 381
		Notes,
		// Token: 0x0400017E RID: 382
		OfficeLocation,
		// Token: 0x0400017F RID: 383
		Profession,
		// Token: 0x04000180 RID: 384
		Spouse,
		// Token: 0x04000181 RID: 385
		User1,
		// Token: 0x04000182 RID: 386
		User2,
		// Token: 0x04000183 RID: 387
		User3,
		// Token: 0x04000184 RID: 388
		User4,
		// Token: 0x04000185 RID: 389
		WebPage,
		// Token: 0x04000186 RID: 390
		CompanyYomi,
		// Token: 0x04000187 RID: 391
		GivenYomi,
		// Token: 0x04000188 RID: 392
		SurnameYomi,
		// Token: 0x04000189 RID: 393
		Account,
		// Token: 0x0400018A RID: 394
		BillingInformation,
		// Token: 0x0400018B RID: 395
		Location,
		// Token: 0x0400018C RID: 396
		Gender,
		// Token: 0x0400018D RID: 397
		Priority,
		// Token: 0x0400018E RID: 398
		Sensitivity,
		// Token: 0x0400018F RID: 399
		IMAddress,
		// Token: 0x04000190 RID: 400
		Schools,
		// Token: 0x04000191 RID: 401
		PersonalWebPage,
		// Token: 0x04000192 RID: 402
		MobilePhone2
	}
}
