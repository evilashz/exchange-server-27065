using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001ED RID: 493
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADContact : ADRecipient, IADOrgPerson, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0006B14F File Offset: 0x0006934F
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADContact.schema;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0006B156 File Offset: 0x00069356
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADContact.MostDerivedClass;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0006B15D File Offset: 0x0006935D
		internal override string ObjectCategoryName
		{
			get
			{
				return ADContact.ObjectCategoryNameInternal;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0006B164 File Offset: 0x00069364
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0006B177 File Offset: 0x00069377
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0006B17E File Offset: 0x0006937E
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0006B181 File Offset: 0x00069381
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (RecipientType.MailContact == base.RecipientType && base.ExternalEmailAddress == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorNullExternalEmailAddress, ADRecipientSchema.ExternalEmailAddress, null));
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0006B1B7 File Offset: 0x000693B7
		internal ADContact(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006B1C1 File Offset: 0x000693C1
		internal ADContact(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0006B1EE File Offset: 0x000693EE
		public ADContact()
		{
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0006B1F6 File Offset: 0x000693F6
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0006B208 File Offset: 0x00069408
		public bool DeliverToForwardingAddress
		{
			get
			{
				return (bool)this[ADContactSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[ADContactSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0006B21B File Offset: 0x0006941B
		public MultiValuedProperty<ADObjectId> CatchAllRecipientBL
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.CatchAllRecipientBL];
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0006B22D File Offset: 0x0006942D
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0006B23F File Offset: 0x0006943F
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.Certificate];
			}
			set
			{
				this[ADRecipientSchema.Certificate] = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0006B24D File Offset: 0x0006944D
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0006B25F File Offset: 0x0006945F
		public MultiValuedProperty<byte[]> UserSMIMECertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.SMimeCertificate];
			}
			set
			{
				this[ADRecipientSchema.SMimeCertificate] = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0006B26D File Offset: 0x0006946D
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0006B27F File Offset: 0x0006947F
		public string C
		{
			get
			{
				return (string)this[ADContactSchema.C];
			}
			set
			{
				this[ADContactSchema.C] = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0006B28D File Offset: 0x0006948D
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0006B29F File Offset: 0x0006949F
		public string City
		{
			get
			{
				return (string)this[ADContactSchema.City];
			}
			set
			{
				this[ADContactSchema.City] = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0006B2AD File Offset: 0x000694AD
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x0006B2BF File Offset: 0x000694BF
		public string Co
		{
			get
			{
				return (string)this[ADContactSchema.Co];
			}
			set
			{
				this[ADContactSchema.Co] = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0006B2CD File Offset: 0x000694CD
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0006B2DF File Offset: 0x000694DF
		public string Company
		{
			get
			{
				return (string)this[ADContactSchema.Company];
			}
			set
			{
				this[ADContactSchema.Company] = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0006B2ED File Offset: 0x000694ED
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0006B2FF File Offset: 0x000694FF
		public int CountryCode
		{
			get
			{
				return (int)this[ADContactSchema.CountryCode];
			}
			set
			{
				this[ADContactSchema.CountryCode] = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0006B312 File Offset: 0x00069512
		public string CountryOrRegionDisplayName
		{
			get
			{
				return (string)this[ADContactSchema.Co];
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0006B324 File Offset: 0x00069524
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x0006B336 File Offset: 0x00069536
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[ADContactSchema.CountryOrRegion];
			}
			set
			{
				this[ADContactSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0006B344 File Offset: 0x00069544
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x0006B356 File Offset: 0x00069556
		public string Department
		{
			get
			{
				return (string)this[ADContactSchema.Department];
			}
			set
			{
				this[ADContactSchema.Department] = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0006B364 File Offset: 0x00069564
		public MultiValuedProperty<ADObjectId> DirectReports
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADContactSchema.DirectReports];
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0006B376 File Offset: 0x00069576
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0006B388 File Offset: 0x00069588
		public string Fax
		{
			get
			{
				return (string)this[ADContactSchema.Fax];
			}
			set
			{
				this[ADContactSchema.Fax] = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0006B396 File Offset: 0x00069596
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0006B3A8 File Offset: 0x000695A8
		public string FirstName
		{
			get
			{
				return (string)this[ADContactSchema.FirstName];
			}
			set
			{
				this[ADContactSchema.FirstName] = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0006B3B6 File Offset: 0x000695B6
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0006B3C8 File Offset: 0x000695C8
		public string HomePhone
		{
			get
			{
				return (string)this[ADContactSchema.HomePhone];
			}
			set
			{
				this[ADContactSchema.HomePhone] = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0006B3D6 File Offset: 0x000695D6
		public MultiValuedProperty<string> IndexedPhoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.IndexedPhoneNumbers];
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0006B3E8 File Offset: 0x000695E8
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0006B3FA File Offset: 0x000695FA
		public string Initials
		{
			get
			{
				return (string)this[ADContactSchema.Initials];
			}
			set
			{
				this[ADContactSchema.Initials] = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0006B408 File Offset: 0x00069608
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0006B41A File Offset: 0x0006961A
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[ADContactSchema.Languages];
			}
			set
			{
				this[ADContactSchema.Languages] = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0006B428 File Offset: 0x00069628
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0006B43A File Offset: 0x0006963A
		public string LastName
		{
			get
			{
				return (string)this[ADContactSchema.LastName];
			}
			set
			{
				this[ADContactSchema.LastName] = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0006B448 File Offset: 0x00069648
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0006B45A File Offset: 0x0006965A
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[ADContactSchema.Manager];
			}
			set
			{
				this[ADContactSchema.Manager] = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0006B468 File Offset: 0x00069668
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x0006B47A File Offset: 0x0006967A
		public string MobilePhone
		{
			get
			{
				return (string)this[ADContactSchema.MobilePhone];
			}
			set
			{
				this[ADContactSchema.MobilePhone] = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0006B488 File Offset: 0x00069688
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0006B49A File Offset: 0x0006969A
		public string Office
		{
			get
			{
				return (string)this[ADContactSchema.Office];
			}
			set
			{
				this[ADContactSchema.Office] = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0006B4A8 File Offset: 0x000696A8
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0006B4BA File Offset: 0x000696BA
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.OtherFax];
			}
			set
			{
				this[ADContactSchema.OtherFax] = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0006B4C8 File Offset: 0x000696C8
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0006B4DA File Offset: 0x000696DA
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.OtherHomePhone];
			}
			set
			{
				this[ADContactSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0006B4E8 File Offset: 0x000696E8
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0006B4FA File Offset: 0x000696FA
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.OtherTelephone];
			}
			set
			{
				this[ADContactSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0006B508 File Offset: 0x00069708
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0006B51A File Offset: 0x0006971A
		public string Pager
		{
			get
			{
				return (string)this[ADContactSchema.Pager];
			}
			set
			{
				this[ADContactSchema.Pager] = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0006B528 File Offset: 0x00069728
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0006B53A File Offset: 0x0006973A
		public string Phone
		{
			get
			{
				return (string)this[ADContactSchema.Phone];
			}
			set
			{
				this[ADContactSchema.Phone] = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0006B548 File Offset: 0x00069748
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0006B55A File Offset: 0x0006975A
		public string PostalCode
		{
			get
			{
				return (string)this[ADContactSchema.PostalCode];
			}
			set
			{
				this[ADContactSchema.PostalCode] = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0006B568 File Offset: 0x00069768
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x0006B57A File Offset: 0x0006977A
		public MultiValuedProperty<string> PostOfficeBox
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.PostOfficeBox];
			}
			set
			{
				this[ADContactSchema.PostOfficeBox] = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0006B588 File Offset: 0x00069788
		public string RtcSipLine
		{
			get
			{
				return (string)this[ADContactSchema.RtcSipLine];
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0006B59A File Offset: 0x0006979A
		public MultiValuedProperty<string> SanitizedPhoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.SanitizedPhoneNumbers];
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0006B5AC File Offset: 0x000697AC
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0006B5BE File Offset: 0x000697BE
		public string StateOrProvince
		{
			get
			{
				return (string)this[ADContactSchema.StateOrProvince];
			}
			set
			{
				this[ADContactSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0006B5CC File Offset: 0x000697CC
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0006B5DE File Offset: 0x000697DE
		public string StreetAddress
		{
			get
			{
				return (string)this[ADContactSchema.StreetAddress];
			}
			set
			{
				this[ADContactSchema.StreetAddress] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0006B5EC File Offset: 0x000697EC
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0006B5FE File Offset: 0x000697FE
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[ADContactSchema.TelephoneAssistant];
			}
			set
			{
				this[ADContactSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0006B60C File Offset: 0x0006980C
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0006B61E File Offset: 0x0006981E
		public string Title
		{
			get
			{
				return (string)this[ADContactSchema.Title];
			}
			set
			{
				this[ADContactSchema.Title] = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0006B62C File Offset: 0x0006982C
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0006B63E File Offset: 0x0006983E
		public MultiValuedProperty<string> UMCallingLineIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.UMCallingLineIds];
			}
			set
			{
				this[ADContactSchema.UMCallingLineIds] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0006B64C File Offset: 0x0006984C
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0006B65E File Offset: 0x0006985E
		public MultiValuedProperty<string> VoiceMailSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADContactSchema.VoiceMailSettings];
			}
			set
			{
				this[ADContactSchema.VoiceMailSettings] = value;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006B66C File Offset: 0x0006986C
		public object[][] GetManagementChainView(bool getPeers, params PropertyDefinition[] returnProperties)
		{
			return ADOrgPerson.GetManagementChainView(base.Session, this, getPeers, returnProperties);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006B67C File Offset: 0x0006987C
		public object[][] GetDirectReportsView(params PropertyDefinition[] returnProperties)
		{
			return ADOrgPerson.GetDirectReportsView(base.Session, this, returnProperties);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006B68C File Offset: 0x0006988C
		public override void PopulateDtmfMap(bool create)
		{
			string text = (this.FirstName + this.LastName).Trim();
			string lastFirst = (this.LastName + this.FirstName).Trim();
			if (string.IsNullOrEmpty(text))
			{
				text = base.DisplayName;
				lastFirst = base.DisplayName;
			}
			base.PopulateDtmfMap(create, text, lastFirst, base.PrimarySmtpAddress, this.SanitizedPhoneNumbers);
		}

		// Token: 0x04000B56 RID: 2902
		private static readonly ADContactSchema schema = ObjectSchema.GetInstance<ADContactSchema>();

		// Token: 0x04000B57 RID: 2903
		internal static string MostDerivedClass = "contact";

		// Token: 0x04000B58 RID: 2904
		internal static string ObjectCategoryNameInternal = "person";
	}
}
