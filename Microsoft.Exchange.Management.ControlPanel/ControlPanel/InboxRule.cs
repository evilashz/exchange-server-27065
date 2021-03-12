using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F9 RID: 1017
	[KnownType(typeof(InboxRule))]
	[DataContract]
	public class InboxRule : RuleRow
	{
		// Token: 0x060033A8 RID: 13224 RVA: 0x000A1E74 File Offset: 0x000A0074
		public InboxRule(InboxRule rule) : base(rule)
		{
			this.Rule = rule;
			base.DescriptionObject = rule.Description;
			base.ConditionDescriptions = base.DescriptionObject.ConditionDescriptions.ToArray();
			base.ActionDescriptions = base.DescriptionObject.ActionDescriptions.ToArray();
			base.ExceptionDescriptions = base.DescriptionObject.ExceptionDescriptions.ToArray();
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000A1EDD File Offset: 0x000A00DD
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x000A1EE5 File Offset: 0x000A00E5
		public InboxRule Rule { get; private set; }

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000A1EEE File Offset: 0x000A00EE
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x000A1F00 File Offset: 0x000A0100
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] From
		{
			get
			{
				return this.Rule.From.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000A1F07 File Offset: 0x000A0107
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x000A1F19 File Offset: 0x000A0119
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] SentTo
		{
			get
			{
				return this.Rule.SentTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000A1F20 File Offset: 0x000A0120
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x000A1F38 File Offset: 0x000A0138
		[DataMember(EmitDefaultValue = false)]
		public Identity FromSubscription
		{
			get
			{
				return this.Rule.FromSubscription.ToIdentity(this.Rule);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x000A1F3F File Offset: 0x000A013F
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x000A1F51 File Offset: 0x000A0151
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectContainsWords
		{
			get
			{
				return this.Rule.SubjectContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000A1F58 File Offset: 0x000A0158
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x000A1F6A File Offset: 0x000A016A
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectOrBodyContainsWords
		{
			get
			{
				return this.Rule.SubjectOrBodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000A1F71 File Offset: 0x000A0171
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000A1F83 File Offset: 0x000A0183
		[DataMember(EmitDefaultValue = false)]
		public string[] FromAddressContainsWords
		{
			get
			{
				return this.Rule.FromAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000A1F8C File Offset: 0x000A018C
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x000A1FB6 File Offset: 0x000A01B6
		[DataMember(EmitDefaultValue = false)]
		public bool? MyNameInToOrCcBox
		{
			get
			{
				if (!this.Rule.MyNameInToOrCcBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000A1FC0 File Offset: 0x000A01C0
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000A1FEA File Offset: 0x000A01EA
		[DataMember(EmitDefaultValue = false)]
		public bool? SentOnlyToMe
		{
			get
			{
				if (!this.Rule.SentOnlyToMe)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000A1FF4 File Offset: 0x000A01F4
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000A201E File Offset: 0x000A021E
		[DataMember(EmitDefaultValue = false)]
		public bool? MyNameInToBox
		{
			get
			{
				if (!this.Rule.MyNameInToBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000A2028 File Offset: 0x000A0228
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000A2052 File Offset: 0x000A0252
		[DataMember(EmitDefaultValue = false)]
		public bool? MyNameInCcBox
		{
			get
			{
				if (!this.Rule.MyNameInCcBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000A205C File Offset: 0x000A025C
		// (set) Token: 0x060033C0 RID: 13248 RVA: 0x000A2086 File Offset: 0x000A0286
		[DataMember(EmitDefaultValue = false)]
		public bool? MyNameNotInToBox
		{
			get
			{
				if (!this.Rule.MyNameNotInToBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000A208D File Offset: 0x000A028D
		// (set) Token: 0x060033C2 RID: 13250 RVA: 0x000A209F File Offset: 0x000A029F
		[DataMember(EmitDefaultValue = false)]
		public string[] BodyContainsWords
		{
			get
			{
				return this.Rule.BodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000A20A6 File Offset: 0x000A02A6
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x000A20B8 File Offset: 0x000A02B8
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientAddressContainsWords
		{
			get
			{
				return this.Rule.RecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000A20BF File Offset: 0x000A02BF
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000A20D1 File Offset: 0x000A02D1
		[DataMember(EmitDefaultValue = false)]
		public string[] HeaderContainsWords
		{
			get
			{
				return this.Rule.HeaderContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000A20D8 File Offset: 0x000A02D8
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000A20EF File Offset: 0x000A02EF
		[DataMember(EmitDefaultValue = false)]
		public string WithImportance
		{
			get
			{
				return this.Rule.WithImportance.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000A20F6 File Offset: 0x000A02F6
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000A210D File Offset: 0x000A030D
		[DataMember(EmitDefaultValue = false)]
		public string WithSensitivity
		{
			get
			{
				return this.Rule.WithSensitivity.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000A2114 File Offset: 0x000A0314
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000A213E File Offset: 0x000A033E
		[DataMember(EmitDefaultValue = false)]
		public bool? HasAttachment
		{
			get
			{
				if (!this.Rule.HasAttachment)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A2145 File Offset: 0x000A0345
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x000A215C File Offset: 0x000A035C
		[DataMember(EmitDefaultValue = false)]
		public string MessageTypeMatches
		{
			get
			{
				return this.Rule.MessageTypeMatches.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000A2163 File Offset: 0x000A0363
		// (set) Token: 0x060033D0 RID: 13264 RVA: 0x000A2175 File Offset: 0x000A0375
		[DataMember(EmitDefaultValue = false)]
		public Identity HasClassification
		{
			get
			{
				return this.Rule.HasClassification.ToIdentity();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000A217C File Offset: 0x000A037C
		// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000A2189 File Offset: 0x000A0389
		[DataMember(EmitDefaultValue = false)]
		public string FlaggedForAction
		{
			get
			{
				return this.Rule.FlaggedForAction;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000A2190 File Offset: 0x000A0390
		// (set) Token: 0x060033D4 RID: 13268 RVA: 0x000A21E2 File Offset: 0x000A03E2
		[DataMember(EmitDefaultValue = false)]
		public NumberRange WithinSizeRange
		{
			get
			{
				NumberRange numberRange = new NumberRange();
				numberRange.AtMost = this.Rule.WithinSizeRangeMaximum.ToKB();
				numberRange.AtLeast = this.Rule.WithinSizeRangeMinimum.ToKB();
				if (numberRange.AtMost == 0 && numberRange.AtLeast == 0)
				{
					return null;
				}
				return numberRange;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000A21EC File Offset: 0x000A03EC
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000A224A File Offset: 0x000A044A
		[DataMember(EmitDefaultValue = false)]
		public DateRange WithinDateRange
		{
			get
			{
				DateRange dateRange = new DateRange();
				dateRange.BeforeDate = this.Rule.ReceivedBeforeDate.ToIdentity();
				dateRange.AfterDate = this.Rule.ReceivedAfterDate.ToIdentity();
				if (dateRange.BeforeDate == null && dateRange.AfterDate == null)
				{
					return null;
				}
				return dateRange;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000A2251 File Offset: 0x000A0451
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000A2272 File Offset: 0x000A0472
		[DataMember(EmitDefaultValue = false)]
		public Identity MoveToFolder
		{
			get
			{
				if (this.Rule.MoveToFolder != null)
				{
					return this.Rule.MoveToFolder.ToIdentity();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000A2279 File Offset: 0x000A0479
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x000A229A File Offset: 0x000A049A
		[DataMember(EmitDefaultValue = false)]
		public Identity CopyToFolder
		{
			get
			{
				if (this.Rule.CopyToFolder != null)
				{
					return this.Rule.CopyToFolder.ToIdentity();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000A22A4 File Offset: 0x000A04A4
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x000A22CE File Offset: 0x000A04CE
		[DataMember(EmitDefaultValue = false)]
		public bool? DeleteMessage
		{
			get
			{
				if (!this.Rule.DeleteMessage)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000A22D8 File Offset: 0x000A04D8
		// (set) Token: 0x060033DE RID: 13278 RVA: 0x000A2302 File Offset: 0x000A0502
		[DataMember(EmitDefaultValue = false)]
		public bool? StopProcessingRules
		{
			get
			{
				if (!this.Rule.StopProcessingRules)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000A230C File Offset: 0x000A050C
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000A234A File Offset: 0x000A054A
		[DataMember(EmitDefaultValue = false)]
		public Identity ApplyCategory
		{
			get
			{
				if (MultiValuedPropertyBase.IsNullOrEmpty(this.Rule.ApplyCategory))
				{
					return null;
				}
				string text = this.Rule.ApplyCategory.ToStringArray().ToCommaSeperatedString();
				return new Identity(text, text);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000A2354 File Offset: 0x000A0554
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000A237E File Offset: 0x000A057E
		[DataMember(EmitDefaultValue = false)]
		public bool? MarkAsRead
		{
			get
			{
				if (!this.Rule.MarkAsRead)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000A2385 File Offset: 0x000A0585
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000A2397 File Offset: 0x000A0597
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ForwardTo
		{
			get
			{
				return this.Rule.ForwardTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000A239E File Offset: 0x000A059E
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000A23D8 File Offset: 0x000A05D8
		[DataMember(EmitDefaultValue = false)]
		public string SendTextMessageNotificationTo
		{
			get
			{
				if (this.Rule.SendTextMessageNotificationTo != null && this.Rule.SendTextMessageNotificationTo.Count > 0)
				{
					return this.Rule.SendTextMessageNotificationTo[0].ToString();
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000A23DF File Offset: 0x000A05DF
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000A23F1 File Offset: 0x000A05F1
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] RedirectTo
		{
			get
			{
				return this.Rule.RedirectTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000A23F8 File Offset: 0x000A05F8
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000A240A File Offset: 0x000A060A
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ForwardAsAttachmentTo
		{
			get
			{
				return this.Rule.ForwardAsAttachmentTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000A2411 File Offset: 0x000A0611
		// (set) Token: 0x060033EC RID: 13292 RVA: 0x000A2428 File Offset: 0x000A0628
		[DataMember(EmitDefaultValue = false)]
		public string MarkImportance
		{
			get
			{
				return this.Rule.MarkImportance.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000A242F File Offset: 0x000A062F
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x000A2441 File Offset: 0x000A0641
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfFrom
		{
			get
			{
				return this.Rule.ExceptIfFrom.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000A2448 File Offset: 0x000A0648
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x000A245A File Offset: 0x000A065A
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfSentTo
		{
			get
			{
				return this.Rule.ExceptIfSentTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000A2461 File Offset: 0x000A0661
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000A2479 File Offset: 0x000A0679
		[DataMember(EmitDefaultValue = false)]
		public Identity ExceptIfFromSubscription
		{
			get
			{
				return this.Rule.ExceptIfFromSubscription.ToIdentity(this.Rule);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000A2480 File Offset: 0x000A0680
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x000A2492 File Offset: 0x000A0692
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectContainsWords
		{
			get
			{
				return this.Rule.ExceptIfSubjectContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000A2499 File Offset: 0x000A0699
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x000A24AB File Offset: 0x000A06AB
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return this.Rule.ExceptIfSubjectOrBodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000A24B2 File Offset: 0x000A06B2
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x000A24C4 File Offset: 0x000A06C4
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return this.Rule.ExceptIfFromAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000A24CC File Offset: 0x000A06CC
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x000A24F6 File Offset: 0x000A06F6
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfMyNameInToOrCcBox
		{
			get
			{
				if (!this.Rule.ExceptIfMyNameInToOrCcBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000A2500 File Offset: 0x000A0700
		// (set) Token: 0x060033FC RID: 13308 RVA: 0x000A252A File Offset: 0x000A072A
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfSentOnlyToMe
		{
			get
			{
				if (!this.Rule.ExceptIfSentOnlyToMe)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000A2534 File Offset: 0x000A0734
		// (set) Token: 0x060033FE RID: 13310 RVA: 0x000A255E File Offset: 0x000A075E
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfMyNameInToBox
		{
			get
			{
				if (!this.Rule.ExceptIfMyNameInToBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000A2568 File Offset: 0x000A0768
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x000A2592 File Offset: 0x000A0792
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfMyNameInCcBox
		{
			get
			{
				if (!this.Rule.ExceptIfMyNameInCcBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000A259C File Offset: 0x000A079C
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000A25C6 File Offset: 0x000A07C6
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfMyNameNotInToBox
		{
			get
			{
				if (!this.Rule.ExceptIfMyNameNotInToBox)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000A25CD File Offset: 0x000A07CD
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000A25DF File Offset: 0x000A07DF
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfBodyContainsWords
		{
			get
			{
				return this.Rule.ExceptIfBodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000A25E6 File Offset: 0x000A07E6
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x000A25F8 File Offset: 0x000A07F8
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return this.Rule.ExceptIfRecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000A25FF File Offset: 0x000A07FF
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x000A2611 File Offset: 0x000A0811
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfHeaderContainsWords
		{
			get
			{
				return this.Rule.ExceptIfHeaderContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000A2618 File Offset: 0x000A0818
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000A262F File Offset: 0x000A082F
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfWithImportance
		{
			get
			{
				return this.Rule.ExceptIfWithImportance.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000A2636 File Offset: 0x000A0836
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000A264D File Offset: 0x000A084D
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfWithSensitivity
		{
			get
			{
				return this.Rule.ExceptIfWithSensitivity.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000A2654 File Offset: 0x000A0854
		// (set) Token: 0x0600340E RID: 13326 RVA: 0x000A267E File Offset: 0x000A087E
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfHasAttachment
		{
			get
			{
				if (!this.Rule.ExceptIfHasAttachment)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000A2685 File Offset: 0x000A0885
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x000A269C File Offset: 0x000A089C
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfMessageTypeMatches
		{
			get
			{
				return this.Rule.ExceptIfMessageTypeMatches.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000A26A3 File Offset: 0x000A08A3
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x000A26B5 File Offset: 0x000A08B5
		[DataMember(EmitDefaultValue = false)]
		public Identity ExceptIfHasClassification
		{
			get
			{
				return this.Rule.ExceptIfHasClassification.ToIdentity();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000A26BC File Offset: 0x000A08BC
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x000A26C9 File Offset: 0x000A08C9
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfFlaggedForAction
		{
			get
			{
				return this.Rule.ExceptIfFlaggedForAction;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000A26D0 File Offset: 0x000A08D0
		// (set) Token: 0x06003416 RID: 13334 RVA: 0x000A2722 File Offset: 0x000A0922
		[DataMember(EmitDefaultValue = false)]
		public NumberRange ExceptIfWithinSizeRange
		{
			get
			{
				NumberRange numberRange = new NumberRange();
				numberRange.AtMost = this.Rule.ExceptIfWithinSizeRangeMaximum.ToKB();
				numberRange.AtLeast = this.Rule.ExceptIfWithinSizeRangeMinimum.ToKB();
				if (numberRange.AtMost == 0 && numberRange.AtLeast == 0)
				{
					return null;
				}
				return numberRange;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000A272C File Offset: 0x000A092C
		// (set) Token: 0x06003418 RID: 13336 RVA: 0x000A278A File Offset: 0x000A098A
		[DataMember(EmitDefaultValue = false)]
		public DateRange ExceptIfWithinDateRange
		{
			get
			{
				DateRange dateRange = new DateRange();
				dateRange.BeforeDate = this.Rule.ExceptIfReceivedBeforeDate.ToIdentity();
				dateRange.AfterDate = this.Rule.ExceptIfReceivedAfterDate.ToIdentity();
				if (dateRange.BeforeDate == null && dateRange.AfterDate == null)
				{
					return null;
				}
				return dateRange;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}
	}
}
