using System;
using System.Collections;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000458 RID: 1112
	[DataContract]
	public abstract class TransportRuleParameters : SetObjectProperties
	{
		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000A95A2 File Offset: 0x000A77A2
		// (set) Token: 0x06003799 RID: 14233 RVA: 0x000A95B4 File Offset: 0x000A77B4
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000A95C2 File Offset: 0x000A77C2
		// (set) Token: 0x0600379B RID: 14235 RVA: 0x000A95D4 File Offset: 0x000A77D4
		[DataMember]
		public string DlpPolicy
		{
			get
			{
				return (string)base["DlpPolicy"];
			}
			set
			{
				base["DlpPolicy"] = value;
			}
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000A95E2 File Offset: 0x000A77E2
		// (set) Token: 0x0600379D RID: 14237 RVA: 0x000A95F4 File Offset: 0x000A77F4
		[DataMember]
		public string Mode
		{
			get
			{
				return (string)base["Mode"];
			}
			set
			{
				base["Mode"] = value;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000A9602 File Offset: 0x000A7802
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x000A9614 File Offset: 0x000A7814
		[DataMember]
		public bool? StopRuleProcessing
		{
			get
			{
				return (bool?)base["StopRuleProcessing"];
			}
			set
			{
				base["StopRuleProcessing"] = (value ?? false);
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000A9646 File Offset: 0x000A7846
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x000A9658 File Offset: 0x000A7858
		[DataMember]
		public DateTime? ActivationDate
		{
			get
			{
				return (DateTime?)base["ActivationDate"];
			}
			set
			{
				if (value != null)
				{
					base["ActivationDate"] = value.Value;
					return;
				}
				base["ActivationDate"] = null;
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000A9687 File Offset: 0x000A7887
		// (set) Token: 0x060037A3 RID: 14243 RVA: 0x000A9699 File Offset: 0x000A7899
		[DataMember]
		public DateTime? ExpiryDate
		{
			get
			{
				return (DateTime?)base["ExpiryDate"];
			}
			set
			{
				if (value != null)
				{
					base["ExpiryDate"] = value.Value;
					return;
				}
				base["ExpiryDate"] = null;
			}
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000A96C8 File Offset: 0x000A78C8
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000A96DA File Offset: 0x000A78DA
		[DataMember]
		public string Comments
		{
			get
			{
				return (string)base["Comments"];
			}
			set
			{
				base["Comments"] = value;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000A96E8 File Offset: 0x000A78E8
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x000A96FA File Offset: 0x000A78FA
		[DataMember]
		public string RuleErrorAction
		{
			get
			{
				return (string)base["RuleErrorAction"];
			}
			set
			{
				base["RuleErrorAction"] = value;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000A9708 File Offset: 0x000A7908
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000A971A File Offset: 0x000A791A
		[DataMember]
		public string SenderAddressLocation
		{
			get
			{
				return (string)base["SenderAddressLocation"];
			}
			set
			{
				base["SenderAddressLocation"] = value;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000A9728 File Offset: 0x000A7928
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000A973A File Offset: 0x000A793A
		[DataMember]
		public int? Priority
		{
			get
			{
				return (int?)base["Priority"];
			}
			set
			{
				base["Priority"] = value;
			}
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000A974D File Offset: 0x000A794D
		// (set) Token: 0x060037AD RID: 14253 RVA: 0x000A975F File Offset: 0x000A795F
		[DataMember]
		public PeopleIdentity[] From
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["From"]);
			}
			set
			{
				base["From"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000A9772 File Offset: 0x000A7972
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x000A9784 File Offset: 0x000A7984
		[DataMember]
		public PeopleIdentity[] FromMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["FromMemberOf"]);
			}
			set
			{
				base["FromMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x000A9797 File Offset: 0x000A7997
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x000A97A9 File Offset: 0x000A79A9
		[DataMember]
		public string FromScope
		{
			get
			{
				return (string)base["FromScope"];
			}
			set
			{
				base["FromScope"] = value;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000A97B7 File Offset: 0x000A79B7
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x000A97C9 File Offset: 0x000A79C9
		[DataMember]
		public PeopleIdentity[] SentTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["SentTo"]);
			}
			set
			{
				base["SentTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000A97DC File Offset: 0x000A79DC
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x000A97EE File Offset: 0x000A79EE
		[DataMember]
		public PeopleIdentity[] SentToMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["SentToMemberOf"]);
			}
			set
			{
				base["SentToMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000A9801 File Offset: 0x000A7A01
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x000A9813 File Offset: 0x000A7A13
		[DataMember]
		public string SentToScope
		{
			get
			{
				return (string)base["SentToScope"];
			}
			set
			{
				base["SentToScope"] = value;
			}
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000A9821 File Offset: 0x000A7A21
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000A9833 File Offset: 0x000A7A33
		[DataMember]
		public PeopleIdentity[] BetweenMemberOf1
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["BetweenMemberOf1"]);
			}
			set
			{
				base["BetweenMemberOf1"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000A9846 File Offset: 0x000A7A46
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000A9858 File Offset: 0x000A7A58
		[DataMember]
		public PeopleIdentity[] BetweenMemberOf2
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["BetweenMemberOf2"]);
			}
			set
			{
				base["BetweenMemberOf2"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000A986B File Offset: 0x000A7A6B
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000A987D File Offset: 0x000A7A7D
		[DataMember]
		public PeopleIdentity[] ManagerAddresses
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ManagerAddresses"]);
			}
			set
			{
				base["ManagerAddresses"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000A9890 File Offset: 0x000A7A90
		// (set) Token: 0x060037BF RID: 14271 RVA: 0x000A98A2 File Offset: 0x000A7AA2
		[DataMember]
		public string ManagerForEvaluatedUser
		{
			get
			{
				return (string)base["ManagerForEvaluatedUser"];
			}
			set
			{
				base["ManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000A98B0 File Offset: 0x000A7AB0
		// (set) Token: 0x060037C1 RID: 14273 RVA: 0x000A98C2 File Offset: 0x000A7AC2
		[DataMember]
		public string SenderManagementRelationship
		{
			get
			{
				return (string)base["SenderManagementRelationship"];
			}
			set
			{
				base["SenderManagementRelationship"] = value;
			}
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000A98D0 File Offset: 0x000A7AD0
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x000A98E2 File Offset: 0x000A7AE2
		[DataMember]
		public string ADComparisonAttribute
		{
			get
			{
				return (string)base["ADComparisonAttribute"];
			}
			set
			{
				base["ADComparisonAttribute"] = value;
			}
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000A98F0 File Offset: 0x000A7AF0
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000A9902 File Offset: 0x000A7B02
		[DataMember]
		public string ADComparisonOperator
		{
			get
			{
				return (string)base["ADComparisonOperator"];
			}
			set
			{
				base["ADComparisonOperator"] = value;
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000A9910 File Offset: 0x000A7B10
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000A9922 File Offset: 0x000A7B22
		[DataMember]
		public PeopleIdentity[] AnyOfToHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfToHeader"]);
			}
			set
			{
				base["AnyOfToHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000A9935 File Offset: 0x000A7B35
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x000A9947 File Offset: 0x000A7B47
		[DataMember]
		public PeopleIdentity[] AnyOfToHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfToHeaderMemberOf"]);
			}
			set
			{
				base["AnyOfToHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000A995A File Offset: 0x000A7B5A
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000A996C File Offset: 0x000A7B6C
		[DataMember]
		public PeopleIdentity[] AnyOfCcHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfCcHeader"]);
			}
			set
			{
				base["AnyOfCcHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000A997F File Offset: 0x000A7B7F
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x000A9991 File Offset: 0x000A7B91
		[DataMember]
		public PeopleIdentity[] AnyOfCcHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfCcHeaderMemberOf"]);
			}
			set
			{
				base["AnyOfCcHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000A99A4 File Offset: 0x000A7BA4
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x000A99B6 File Offset: 0x000A7BB6
		[DataMember]
		public PeopleIdentity[] AnyOfToCcHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfToCcHeader"]);
			}
			set
			{
				base["AnyOfToCcHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000A99C9 File Offset: 0x000A7BC9
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000A99DB File Offset: 0x000A7BDB
		[DataMember]
		public PeopleIdentity[] AnyOfToCcHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AnyOfToCcHeaderMemberOf"]);
			}
			set
			{
				base["AnyOfToCcHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000A99F0 File Offset: 0x000A7BF0
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000A9A19 File Offset: 0x000A7C19
		[DataMember]
		public Identity HasClassification
		{
			get
			{
				string value = ((string[])base["HasClassification"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["HasClassification"] = ((value != null) ? value.RawIdentity : null);
			}
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000A9A38 File Offset: 0x000A7C38
		// (set) Token: 0x060037D5 RID: 14293 RVA: 0x000A9A61 File Offset: 0x000A7C61
		[DataMember]
		public Identity SenderInRecipientList
		{
			get
			{
				string value = ((string[])base["SenderInRecipientList"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["SenderInRecipientList"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000A9A74 File Offset: 0x000A7C74
		// (set) Token: 0x060037D7 RID: 14295 RVA: 0x000A9A9D File Offset: 0x000A7C9D
		[DataMember]
		public Identity RecipientInSenderList
		{
			get
			{
				string value = ((string[])base["RecipientInSenderList"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["RecipientInSenderList"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000A9AB0 File Offset: 0x000A7CB0
		// (set) Token: 0x060037D9 RID: 14297 RVA: 0x000A9AC2 File Offset: 0x000A7CC2
		[DataMember]
		public string[] SubjectContainsWords
		{
			get
			{
				return (string[])base["SubjectContainsWords"];
			}
			set
			{
				base["SubjectContainsWords"] = value;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x000A9AD0 File Offset: 0x000A7CD0
		// (set) Token: 0x060037DB RID: 14299 RVA: 0x000A9AE2 File Offset: 0x000A7CE2
		[DataMember]
		public string[] SubjectOrBodyContainsWords
		{
			get
			{
				return (string[])base["SubjectOrBodyContainsWords"];
			}
			set
			{
				base["SubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x000A9AF0 File Offset: 0x000A7CF0
		// (set) Token: 0x060037DD RID: 14301 RVA: 0x000A9B02 File Offset: 0x000A7D02
		[DataMember]
		public string HeaderContainsMessageHeader
		{
			get
			{
				return (string)base["HeaderContainsMessageHeader"];
			}
			set
			{
				base["HeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x000A9B10 File Offset: 0x000A7D10
		// (set) Token: 0x060037DF RID: 14303 RVA: 0x000A9B22 File Offset: 0x000A7D22
		[DataMember]
		public string[] HeaderContainsWords
		{
			get
			{
				return (string[])base["HeaderContainsWords"];
			}
			set
			{
				base["HeaderContainsWords"] = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000A9B30 File Offset: 0x000A7D30
		// (set) Token: 0x060037E1 RID: 14305 RVA: 0x000A9B42 File Offset: 0x000A7D42
		[DataMember]
		public string[] FromAddressContainsWords
		{
			get
			{
				return (string[])base["FromAddressContainsWords"];
			}
			set
			{
				base["FromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000A9B50 File Offset: 0x000A7D50
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000A9B62 File Offset: 0x000A7D62
		[DataMember]
		public Hashtable[] MessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base["MessageContainsDataClassifications"];
			}
			set
			{
				base["MessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000A9B70 File Offset: 0x000A7D70
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x000A9B82 File Offset: 0x000A7D82
		[DataMember]
		public string[] RecipientAddressContainsWords
		{
			get
			{
				return (string[])base["RecipientAddressContainsWords"];
			}
			set
			{
				base["RecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000A9B90 File Offset: 0x000A7D90
		// (set) Token: 0x060037E7 RID: 14311 RVA: 0x000A9BA2 File Offset: 0x000A7DA2
		[DataMember]
		public string[] AnyOfRecipientAddressContainsWords
		{
			get
			{
				return (string[])base["AnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base["AnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000A9BB0 File Offset: 0x000A7DB0
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000A9BC2 File Offset: 0x000A7DC2
		[DataMember]
		public string[] AttachmentContainsWords
		{
			get
			{
				return (string[])base["AttachmentContainsWords"];
			}
			set
			{
				base["AttachmentContainsWords"] = value;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000A9BD0 File Offset: 0x000A7DD0
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x000A9BE4 File Offset: 0x000A7DE4
		[DataMember]
		public bool? HasNoClassification
		{
			get
			{
				return (bool?)base["HasNoClassification"];
			}
			set
			{
				base["HasNoClassification"] = (value ?? false);
			}
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000A9C16 File Offset: 0x000A7E16
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x000A9C28 File Offset: 0x000A7E28
		[DataMember]
		public bool? AttachmentIsUnsupported
		{
			get
			{
				return (bool?)base["AttachmentIsUnsupported"];
			}
			set
			{
				base["AttachmentIsUnsupported"] = (value ?? false);
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000A9C5A File Offset: 0x000A7E5A
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x000A9C6C File Offset: 0x000A7E6C
		[DataMember]
		public string[] SenderADAttributeContainsWords
		{
			get
			{
				return (string[])base["SenderADAttributeContainsWords"];
			}
			set
			{
				base["SenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000A9C7A File Offset: 0x000A7E7A
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000A9C8C File Offset: 0x000A7E8C
		[DataMember]
		public string[] RecipientADAttributeContainsWords
		{
			get
			{
				return (string[])base["RecipientADAttributeContainsWords"];
			}
			set
			{
				base["RecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000A9C9A File Offset: 0x000A7E9A
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x000A9CAC File Offset: 0x000A7EAC
		[DataMember]
		public bool? AttachmentHasExecutableContent
		{
			get
			{
				return (bool?)base["AttachmentHasExecutableContent"];
			}
			set
			{
				base["AttachmentHasExecutableContent"] = (value ?? false);
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000A9CDE File Offset: 0x000A7EDE
		// (set) Token: 0x060037F5 RID: 14325 RVA: 0x000A9CF0 File Offset: 0x000A7EF0
		[DataMember]
		public string[] SubjectMatchesPatterns
		{
			get
			{
				return (string[])base["SubjectMatchesPatterns"];
			}
			set
			{
				base["SubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x000A9CFE File Offset: 0x000A7EFE
		// (set) Token: 0x060037F7 RID: 14327 RVA: 0x000A9D10 File Offset: 0x000A7F10
		[DataMember]
		public string[] SubjectOrBodyMatchesPatterns
		{
			get
			{
				return (string[])base["SubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base["SubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000A9D1E File Offset: 0x000A7F1E
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x000A9D30 File Offset: 0x000A7F30
		[DataMember]
		public string HeaderMatchesMessageHeader
		{
			get
			{
				return (string)base["HeaderMatchesMessageHeader"];
			}
			set
			{
				base["HeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000A9D3E File Offset: 0x000A7F3E
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x000A9D50 File Offset: 0x000A7F50
		[DataMember]
		public string[] HeaderMatchesPatterns
		{
			get
			{
				return (string[])base["HeaderMatchesPatterns"];
			}
			set
			{
				base["HeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000A9D5E File Offset: 0x000A7F5E
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000A9D70 File Offset: 0x000A7F70
		[DataMember]
		public string[] FromAddressMatchesPatterns
		{
			get
			{
				return (string[])base["FromAddressMatchesPatterns"];
			}
			set
			{
				base["FromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000A9D7E File Offset: 0x000A7F7E
		// (set) Token: 0x060037FF RID: 14335 RVA: 0x000A9D90 File Offset: 0x000A7F90
		[DataMember]
		public string[] AttachmentNameMatchesPatterns
		{
			get
			{
				return (string[])base["AttachmentNameMatchesPatterns"];
			}
			set
			{
				base["AttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000A9D9E File Offset: 0x000A7F9E
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x000A9DB0 File Offset: 0x000A7FB0
		[DataMember]
		public string[] AttachmentExtensionMatchesWords
		{
			get
			{
				return (string[])base["AttachmentExtensionMatchesWords"];
			}
			set
			{
				base["AttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x000A9DBE File Offset: 0x000A7FBE
		// (set) Token: 0x06003803 RID: 14339 RVA: 0x000A9DD0 File Offset: 0x000A7FD0
		[DataMember]
		public string[] AttachmentMatchesPatterns
		{
			get
			{
				return (string[])base["AttachmentMatchesPatterns"];
			}
			set
			{
				base["AttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x000A9DDE File Offset: 0x000A7FDE
		// (set) Token: 0x06003805 RID: 14341 RVA: 0x000A9DF0 File Offset: 0x000A7FF0
		[DataMember]
		public string[] RecipientAddressMatchesPatterns
		{
			get
			{
				return (string[])base["RecipientAddressMatchesPatterns"];
			}
			set
			{
				base["RecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000A9DFE File Offset: 0x000A7FFE
		// (set) Token: 0x06003807 RID: 14343 RVA: 0x000A9E10 File Offset: 0x000A8010
		[DataMember]
		public string[] AnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (string[])base["AnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base["AnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000A9E1E File Offset: 0x000A801E
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x000A9E30 File Offset: 0x000A8030
		[DataMember]
		public string[] SenderADAttributeMatchesPatterns
		{
			get
			{
				return (string[])base["SenderADAttributeMatchesPatterns"];
			}
			set
			{
				base["SenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000A9E3E File Offset: 0x000A803E
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x000A9E50 File Offset: 0x000A8050
		[DataMember]
		public string[] RecipientADAttributeMatchesPatterns
		{
			get
			{
				return (string[])base["RecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base["RecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000A9E5E File Offset: 0x000A805E
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x000A9E70 File Offset: 0x000A8070
		[DataMember]
		public string[] SenderIpRanges
		{
			get
			{
				return (string[])base["SenderIpRanges"];
			}
			set
			{
				base["SenderIpRanges"] = value;
			}
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000A9E7E File Offset: 0x000A807E
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x000A9E90 File Offset: 0x000A8090
		[DataMember]
		public string[] RecipientDomainIs
		{
			get
			{
				return (string[])base["RecipientDomainIs"];
			}
			set
			{
				base["RecipientDomainIs"] = value;
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000A9E9E File Offset: 0x000A809E
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x000A9EB0 File Offset: 0x000A80B0
		[DataMember]
		public string[] SenderDomainIs
		{
			get
			{
				return (string[])base["SenderDomainIs"];
			}
			set
			{
				base["SenderDomainIs"] = value;
			}
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x000A9EBE File Offset: 0x000A80BE
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x000A9ED0 File Offset: 0x000A80D0
		[DataMember]
		public string[] ContentCharacterSetContainsWords
		{
			get
			{
				return (string[])base["ContentCharacterSetContainsWords"];
			}
			set
			{
				base["ContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000A9EDE File Offset: 0x000A80DE
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x000A9EF0 File Offset: 0x000A80F0
		[DataMember]
		public int? SCLOver
		{
			get
			{
				return (int?)base["SCLOver"];
			}
			set
			{
				base["SCLOver"] = value;
			}
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000A9F03 File Offset: 0x000A8103
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x000A9F15 File Offset: 0x000A8115
		[DataMember]
		public long? AttachmentSizeOver
		{
			get
			{
				return (long?)base["AttachmentSizeOver"];
			}
			set
			{
				if (value != null)
				{
					base["AttachmentSizeOver"] = ByteQuantifiedSize.FromKB((ulong)value.Value);
					return;
				}
				base["AttachmentSizeOver"] = null;
			}
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000A9F49 File Offset: 0x000A8149
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x000A9F5C File Offset: 0x000A815C
		[DataMember]
		public bool? AttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool?)base["AttachmentProcessingLimitExceeded"];
			}
			set
			{
				base["AttachmentProcessingLimitExceeded"] = (value ?? false);
			}
		}

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000A9F8E File Offset: 0x000A818E
		// (set) Token: 0x0600381B RID: 14363 RVA: 0x000A9FA0 File Offset: 0x000A81A0
		[DataMember]
		public long? MessageSizeOver
		{
			get
			{
				return (long?)base["MessageSizeOver"];
			}
			set
			{
				if (value != null)
				{
					base["MessageSizeOver"] = ByteQuantifiedSize.FromKB((ulong)value.Value);
					return;
				}
				base["MessageSizeOver"] = null;
			}
		}

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000A9FD4 File Offset: 0x000A81D4
		// (set) Token: 0x0600381D RID: 14365 RVA: 0x000A9FE6 File Offset: 0x000A81E6
		[DataMember]
		public string SetAuditSeverity
		{
			get
			{
				return (string)base["SetAuditSeverity"];
			}
			set
			{
				base["SetAuditSeverity"] = value;
			}
		}

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000A9FF4 File Offset: 0x000A81F4
		// (set) Token: 0x0600381F RID: 14367 RVA: 0x000AA006 File Offset: 0x000A8206
		[DataMember]
		public string WithImportance
		{
			get
			{
				return (string)base["WithImportance"];
			}
			set
			{
				base["WithImportance"] = value;
			}
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000AA014 File Offset: 0x000A8214
		// (set) Token: 0x06003821 RID: 14369 RVA: 0x000AA026 File Offset: 0x000A8226
		[DataMember]
		public string MessageTypeMatches
		{
			get
			{
				return (string)base["MessageTypeMatches"];
			}
			set
			{
				base["MessageTypeMatches"] = value;
			}
		}

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x000AA034 File Offset: 0x000A8234
		// (set) Token: 0x06003823 RID: 14371 RVA: 0x000AA048 File Offset: 0x000A8248
		[DataMember]
		public bool? HasSenderOverride
		{
			get
			{
				return (bool?)base["HasSenderOverride"];
			}
			set
			{
				base["HasSenderOverride"] = (value ?? false);
			}
		}

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x06003824 RID: 14372 RVA: 0x000AA07A File Offset: 0x000A827A
		// (set) Token: 0x06003825 RID: 14373 RVA: 0x000AA08C File Offset: 0x000A828C
		[DataMember]
		public bool? AttachmentIsPasswordProtected
		{
			get
			{
				return (bool?)base["AttachmentIsPasswordProtected"];
			}
			set
			{
				base["AttachmentIsPasswordProtected"] = (value ?? false);
			}
		}

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000AA0BE File Offset: 0x000A82BE
		// (set) Token: 0x06003827 RID: 14375 RVA: 0x000AA0D0 File Offset: 0x000A82D0
		[DataMember]
		public string PrependSubject
		{
			get
			{
				return (string)base["PrependSubject"];
			}
			set
			{
				base["PrependSubject"] = value;
			}
		}

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000AA0DE File Offset: 0x000A82DE
		// (set) Token: 0x06003829 RID: 14377 RVA: 0x000AA0F0 File Offset: 0x000A82F0
		[DataMember]
		public string ApplyHtmlDisclaimerLocation
		{
			get
			{
				return (string)base["ApplyHtmlDisclaimerLocation"];
			}
			set
			{
				base["ApplyHtmlDisclaimerLocation"] = value;
			}
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000AA0FE File Offset: 0x000A82FE
		// (set) Token: 0x0600382B RID: 14379 RVA: 0x000AA110 File Offset: 0x000A8310
		[DataMember]
		public string ApplyHtmlDisclaimerText
		{
			get
			{
				return (string)base["ApplyHtmlDisclaimerText"];
			}
			set
			{
				base["ApplyHtmlDisclaimerText"] = value;
			}
		}

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x0600382C RID: 14380 RVA: 0x000AA11E File Offset: 0x000A831E
		// (set) Token: 0x0600382D RID: 14381 RVA: 0x000AA130 File Offset: 0x000A8330
		[DataMember]
		public string ApplyHtmlDisclaimerFallbackAction
		{
			get
			{
				return (string)base["ApplyHtmlDisclaimerFallbackAction"];
			}
			set
			{
				base["ApplyHtmlDisclaimerFallbackAction"] = value;
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000AA13E File Offset: 0x000A833E
		// (set) Token: 0x0600382F RID: 14383 RVA: 0x000AA150 File Offset: 0x000A8350
		[DataMember]
		public string SetHeaderName
		{
			get
			{
				return (string)base["SetHeaderName"];
			}
			set
			{
				base["SetHeaderName"] = value;
			}
		}

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x000AA15E File Offset: 0x000A835E
		// (set) Token: 0x06003831 RID: 14385 RVA: 0x000AA170 File Offset: 0x000A8370
		[DataMember]
		public string SetHeaderValue
		{
			get
			{
				return (string)base["SetHeaderValue"];
			}
			set
			{
				base["SetHeaderValue"] = value;
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000AA17E File Offset: 0x000A837E
		// (set) Token: 0x06003833 RID: 14387 RVA: 0x000AA190 File Offset: 0x000A8390
		[DataMember]
		public string RemoveHeader
		{
			get
			{
				return (string)base["RemoveHeader"];
			}
			set
			{
				base["RemoveHeader"] = value;
			}
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000AA1A0 File Offset: 0x000A83A0
		// (set) Token: 0x06003835 RID: 14389 RVA: 0x000AA1C9 File Offset: 0x000A83C9
		[DataMember]
		public Identity ApplyClassification
		{
			get
			{
				string value = ((string[])base["ApplyClassification"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ApplyClassification"] = ((value != null) ? value.RawIdentity : null);
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000AA1E8 File Offset: 0x000A83E8
		// (set) Token: 0x06003837 RID: 14391 RVA: 0x000AA1FA File Offset: 0x000A83FA
		[DataMember]
		public int? SetSCL
		{
			get
			{
				return (int?)base["SetSCL"];
			}
			set
			{
				base["SetSCL"] = value;
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000AA210 File Offset: 0x000A8410
		// (set) Token: 0x06003839 RID: 14393 RVA: 0x000AA234 File Offset: 0x000A8434
		[DataMember]
		public Identity ApplyRightsProtectionTemplate
		{
			get
			{
				string value = (string)base["ApplyRightsProtectionTemplate"];
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ApplyRightsProtectionTemplate"] = value;
			}
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x000AA242 File Offset: 0x000A8442
		// (set) Token: 0x0600383B RID: 14395 RVA: 0x000AA254 File Offset: 0x000A8454
		[DataMember]
		public PeopleIdentity[] AddToRecipients
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["AddToRecipients"]);
			}
			set
			{
				base["AddToRecipients"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000AA267 File Offset: 0x000A8467
		// (set) Token: 0x0600383D RID: 14397 RVA: 0x000AA279 File Offset: 0x000A8479
		[DataMember]
		public PeopleIdentity[] CopyTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["CopyTo"]);
			}
			set
			{
				base["CopyTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000AA28C File Offset: 0x000A848C
		// (set) Token: 0x0600383F RID: 14399 RVA: 0x000AA29E File Offset: 0x000A849E
		[DataMember]
		public PeopleIdentity[] BlindCopyTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["BlindCopyTo"]);
			}
			set
			{
				base["BlindCopyTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000AA2B1 File Offset: 0x000A84B1
		// (set) Token: 0x06003841 RID: 14401 RVA: 0x000AA2C3 File Offset: 0x000A84C3
		[DataMember]
		public string AddManagerAsRecipientType
		{
			get
			{
				return (string)base["AddManagerAsRecipientType"];
			}
			set
			{
				base["AddManagerAsRecipientType"] = value;
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000AA2D1 File Offset: 0x000A84D1
		// (set) Token: 0x06003843 RID: 14403 RVA: 0x000AA2E3 File Offset: 0x000A84E3
		[DataMember]
		public PeopleIdentity[] ModerateMessageByUser
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ModerateMessageByUser"]);
			}
			set
			{
				base["ModerateMessageByUser"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000AA2F6 File Offset: 0x000A84F6
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x000AA308 File Offset: 0x000A8508
		[DataMember]
		public bool? ModerateMessageByManager
		{
			get
			{
				return (bool?)base["ModerateMessageByManager"];
			}
			set
			{
				base["ModerateMessageByManager"] = (value ?? false);
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000AA33A File Offset: 0x000A853A
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x000AA34C File Offset: 0x000A854C
		[DataMember]
		public PeopleIdentity[] RedirectMessageTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["RedirectMessageTo"]);
			}
			set
			{
				base["RedirectMessageTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000AA35F File Offset: 0x000A855F
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x000AA380 File Offset: 0x000A8580
		[DataMember]
		public string RejectMessageEnhancedStatusCode
		{
			get
			{
				if (this.SenderNotifySettings.NotifySender != null)
				{
					return null;
				}
				return (string)base["RejectMessageEnhancedStatusCode"];
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base["RejectMessageEnhancedStatusCode"] = value;
				}
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000AA396 File Offset: 0x000A8596
		// (set) Token: 0x0600384B RID: 14411 RVA: 0x000AA3B2 File Offset: 0x000A85B2
		[DataMember]
		public string RejectMessageReasonText
		{
			get
			{
				if (this.SenderNotifySettings != null)
				{
					return null;
				}
				return (string)base["RejectMessageReasonText"];
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base["RejectMessageReasonText"] = value;
				}
			}
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x000AA3C8 File Offset: 0x000A85C8
		// (set) Token: 0x0600384D RID: 14413 RVA: 0x000AA3DC File Offset: 0x000A85DC
		[DataMember]
		public bool? DeleteMessage
		{
			get
			{
				return (bool?)base["DeleteMessage"];
			}
			set
			{
				base["DeleteMessage"] = (value ?? false);
			}
		}

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000AA40E File Offset: 0x000A860E
		// (set) Token: 0x0600384F RID: 14415 RVA: 0x000AA420 File Offset: 0x000A8620
		[DataMember]
		public Identity GenerateIncidentReport
		{
			get
			{
				return (Identity)base["GenerateIncidentReport"];
			}
			set
			{
				base["GenerateIncidentReport"] = value;
			}
		}

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000AA42E File Offset: 0x000A862E
		// (set) Token: 0x06003851 RID: 14417 RVA: 0x000AA440 File Offset: 0x000A8640
		[DataMember]
		public string IncidentReportOriginalMail
		{
			get
			{
				return (string)base["IncidentReportOriginalMail"];
			}
			set
			{
				base["IncidentReportOriginalMail"] = value;
			}
		}

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000AA44E File Offset: 0x000A864E
		// (set) Token: 0x06003853 RID: 14419 RVA: 0x000AA460 File Offset: 0x000A8660
		[DataMember]
		public string[] IncidentReportContent
		{
			get
			{
				return (string[])base["IncidentReportContent"];
			}
			set
			{
				base["IncidentReportContent"] = value;
			}
		}

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000AA46E File Offset: 0x000A866E
		// (set) Token: 0x06003855 RID: 14421 RVA: 0x000AA480 File Offset: 0x000A8680
		[DataMember]
		public string GenerateNotification
		{
			get
			{
				return (string)base["GenerateNotification"];
			}
			set
			{
				base["GenerateNotification"] = value;
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000AA48E File Offset: 0x000A868E
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x000AA4A0 File Offset: 0x000A86A0
		[DataMember]
		public bool? RouteMessageOutboundRequireTls
		{
			get
			{
				return (bool?)base["RouteMessageOutboundRequireTls"];
			}
			set
			{
				base["RouteMessageOutboundRequireTls"] = (value ?? false);
			}
		}

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000AA4D2 File Offset: 0x000A86D2
		// (set) Token: 0x06003859 RID: 14425 RVA: 0x000AA4E4 File Offset: 0x000A86E4
		[DataMember]
		public bool? ApplyOME
		{
			get
			{
				return (bool?)base["ApplyOME"];
			}
			set
			{
				base["ApplyOME"] = (value ?? false);
			}
		}

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000AA516 File Offset: 0x000A8716
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x000AA528 File Offset: 0x000A8728
		[DataMember]
		public bool? RemoveOME
		{
			get
			{
				return (bool?)base["RemoveOME"];
			}
			set
			{
				base["RemoveOME"] = (value ?? false);
			}
		}

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000AA55A File Offset: 0x000A875A
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x000AA56C File Offset: 0x000A876C
		[DataMember]
		public Identity RouteMessageOutboundConnector
		{
			get
			{
				return (Identity)base["RouteMessageOutboundConnector"];
			}
			set
			{
				base["RouteMessageOutboundConnector"] = value;
			}
		}

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000AA57C File Offset: 0x000A877C
		// (set) Token: 0x0600385F RID: 14431 RVA: 0x000AA5BC File Offset: 0x000A87BC
		[DataMember]
		public SenderNotifySettings SenderNotifySettings
		{
			get
			{
				return new SenderNotifySettings
				{
					NotifySender = (string)base["NotifySender"],
					RejectMessage = (string)base["RejectMessageReasonText"]
				};
			}
			set
			{
				if (value != null)
				{
					base["NotifySender"] = value.NotifySender;
					if (value.RejectMessage.ToStringWithNull() != null)
					{
						base["RejectMessageReasonText"] = value.RejectMessage.ToStringWithNull();
						return;
					}
				}
				else
				{
					base["NotifySender"] = null;
					base["RejectMessageReasonText"] = null;
				}
			}
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000AA619 File Offset: 0x000A8819
		// (set) Token: 0x06003861 RID: 14433 RVA: 0x000AA62C File Offset: 0x000A882C
		[DataMember]
		public bool? Quarantine
		{
			get
			{
				return (bool?)base["Quarantine"];
			}
			set
			{
				base["Quarantine"] = (value ?? false);
			}
		}

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000AA65E File Offset: 0x000A885E
		// (set) Token: 0x06003863 RID: 14435 RVA: 0x000AA670 File Offset: 0x000A8870
		[DataMember]
		public PeopleIdentity[] ExceptIfFrom
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfFrom"]);
			}
			set
			{
				base["ExceptIfFrom"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000AA683 File Offset: 0x000A8883
		// (set) Token: 0x06003865 RID: 14437 RVA: 0x000AA695 File Offset: 0x000A8895
		[DataMember]
		public PeopleIdentity[] ExceptIfFromMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfFromMemberOf"]);
			}
			set
			{
				base["ExceptIfFromMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000AA6A8 File Offset: 0x000A88A8
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x000AA6BA File Offset: 0x000A88BA
		[DataMember]
		public string ExceptIfFromScope
		{
			get
			{
				return (string)base["ExceptIfFromScope"];
			}
			set
			{
				base["ExceptIfFromScope"] = value;
			}
		}

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000AA6C8 File Offset: 0x000A88C8
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x000AA6DA File Offset: 0x000A88DA
		[DataMember]
		public PeopleIdentity[] ExceptIfSentTo
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfSentTo"]);
			}
			set
			{
				base["ExceptIfSentTo"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000AA6ED File Offset: 0x000A88ED
		// (set) Token: 0x0600386B RID: 14443 RVA: 0x000AA6FF File Offset: 0x000A88FF
		[DataMember]
		public PeopleIdentity[] ExceptIfSentToMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfSentToMemberOf"]);
			}
			set
			{
				base["ExceptIfSentToMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000AA712 File Offset: 0x000A8912
		// (set) Token: 0x0600386D RID: 14445 RVA: 0x000AA724 File Offset: 0x000A8924
		[DataMember]
		public string ExceptIfSentToScope
		{
			get
			{
				return (string)base["ExceptIfSentToScope"];
			}
			set
			{
				base["ExceptIfSentToScope"] = value;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000AA732 File Offset: 0x000A8932
		// (set) Token: 0x0600386F RID: 14447 RVA: 0x000AA744 File Offset: 0x000A8944
		[DataMember]
		public PeopleIdentity[] ExceptIfBetweenMemberOf1
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfBetweenMemberOf1"]);
			}
			set
			{
				base["ExceptIfBetweenMemberOf1"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000AA757 File Offset: 0x000A8957
		// (set) Token: 0x06003871 RID: 14449 RVA: 0x000AA769 File Offset: 0x000A8969
		[DataMember]
		public PeopleIdentity[] ExceptIfBetweenMemberOf2
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfBetweenMemberOf2"]);
			}
			set
			{
				base["ExceptIfBetweenMemberOf2"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000AA77C File Offset: 0x000A897C
		// (set) Token: 0x06003873 RID: 14451 RVA: 0x000AA78E File Offset: 0x000A898E
		[DataMember]
		public PeopleIdentity[] ExceptIfManagerAddresses
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfManagerAddresses"]);
			}
			set
			{
				base["ExceptIfManagerAddresses"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000AA7A1 File Offset: 0x000A89A1
		// (set) Token: 0x06003875 RID: 14453 RVA: 0x000AA7B3 File Offset: 0x000A89B3
		[DataMember]
		public string ExceptIfManagerForEvaluatedUser
		{
			get
			{
				return (string)base["ExceptIfManagerForEvaluatedUser"];
			}
			set
			{
				base["ExceptIfManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000AA7C1 File Offset: 0x000A89C1
		// (set) Token: 0x06003877 RID: 14455 RVA: 0x000AA7D3 File Offset: 0x000A89D3
		[DataMember]
		public string ExceptIfSenderManagementRelationship
		{
			get
			{
				return (string)base["ExceptIfSenderManagementRelationship"];
			}
			set
			{
				base["ExceptIfSenderManagementRelationship"] = value;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000AA7E1 File Offset: 0x000A89E1
		// (set) Token: 0x06003879 RID: 14457 RVA: 0x000AA7F3 File Offset: 0x000A89F3
		[DataMember]
		public string ExceptIfADComparisonAttribute
		{
			get
			{
				return (string)base["ExceptIfADComparisonAttribute"];
			}
			set
			{
				base["ExceptIfADComparisonAttribute"] = value;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000AA801 File Offset: 0x000A8A01
		// (set) Token: 0x0600387B RID: 14459 RVA: 0x000AA813 File Offset: 0x000A8A13
		[DataMember]
		public string ExceptIfADComparisonOperator
		{
			get
			{
				return (string)base["ExceptIfADComparisonOperator"];
			}
			set
			{
				base["ExceptIfADComparisonOperator"] = value;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000AA824 File Offset: 0x000A8A24
		// (set) Token: 0x0600387D RID: 14461 RVA: 0x000AA84D File Offset: 0x000A8A4D
		[DataMember]
		public Identity ExceptIfSenderInRecipientList
		{
			get
			{
				string value = ((string[])base["ExceptIfSenderInRecipientList"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ExceptIfSenderInRecipientList"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000AA860 File Offset: 0x000A8A60
		// (set) Token: 0x0600387F RID: 14463 RVA: 0x000AA889 File Offset: 0x000A8A89
		[DataMember]
		public Identity ExceptIfRecipientInSenderList
		{
			get
			{
				string value = ((string[])base["ExceptIfRecipientInSenderList"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ExceptIfRecipientInSenderList"] = value.ToTaskIdStringArray();
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000AA89C File Offset: 0x000A8A9C
		// (set) Token: 0x06003881 RID: 14465 RVA: 0x000AA8AE File Offset: 0x000A8AAE
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfToHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfToHeader"]);
			}
			set
			{
				base["ExceptIfAnyOfToHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000AA8C1 File Offset: 0x000A8AC1
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x000AA8D3 File Offset: 0x000A8AD3
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfToHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfToHeaderMemberOf"]);
			}
			set
			{
				base["ExceptIfAnyOfToHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000AA8E6 File Offset: 0x000A8AE6
		// (set) Token: 0x06003885 RID: 14469 RVA: 0x000AA8F8 File Offset: 0x000A8AF8
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfCcHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfCcHeader"]);
			}
			set
			{
				base["ExceptIfAnyOfCcHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000AA90B File Offset: 0x000A8B0B
		// (set) Token: 0x06003887 RID: 14471 RVA: 0x000AA91D File Offset: 0x000A8B1D
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfCcHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfCcHeaderMemberOf"]);
			}
			set
			{
				base["ExceptIfAnyOfCcHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000AA930 File Offset: 0x000A8B30
		// (set) Token: 0x06003889 RID: 14473 RVA: 0x000AA942 File Offset: 0x000A8B42
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfToCcHeader
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfToCcHeader"]);
			}
			set
			{
				base["ExceptIfAnyOfToCcHeader"] = value.ToIdParameters();
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000AA955 File Offset: 0x000A8B55
		// (set) Token: 0x0600388B RID: 14475 RVA: 0x000AA967 File Offset: 0x000A8B67
		[DataMember]
		public PeopleIdentity[] ExceptIfAnyOfToCcHeaderMemberOf
		{
			get
			{
				return PeopleIdentity.FromIdParameters(base["ExceptIfAnyOfToCcHeaderMemberOf"]);
			}
			set
			{
				base["ExceptIfAnyOfToCcHeaderMemberOf"] = value.ToIdParameters();
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000AA97C File Offset: 0x000A8B7C
		// (set) Token: 0x0600388D RID: 14477 RVA: 0x000AA9A5 File Offset: 0x000A8BA5
		[DataMember]
		public Identity ExceptIfHasClassification
		{
			get
			{
				string value = ((string[])base["ExceptIfHasClassification"]).ToCommaSeperatedString();
				return Identity.FromIdParameter(value);
			}
			set
			{
				base["ExceptIfHasClassification"] = ((value != null) ? value.RawIdentity : null);
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000AA9C4 File Offset: 0x000A8BC4
		// (set) Token: 0x0600388F RID: 14479 RVA: 0x000AA9D6 File Offset: 0x000A8BD6
		[DataMember]
		public string[] ExceptIfSubjectContainsWords
		{
			get
			{
				return (string[])base["ExceptIfSubjectContainsWords"];
			}
			set
			{
				base["ExceptIfSubjectContainsWords"] = value;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		// (set) Token: 0x06003891 RID: 14481 RVA: 0x000AA9F6 File Offset: 0x000A8BF6
		[DataMember]
		public string[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return (string[])base["ExceptIfSubjectOrBodyContainsWords"];
			}
			set
			{
				base["ExceptIfSubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000AAA04 File Offset: 0x000A8C04
		// (set) Token: 0x06003893 RID: 14483 RVA: 0x000AAA16 File Offset: 0x000A8C16
		[DataMember]
		public string ExceptIfHeaderContainsMessageHeader
		{
			get
			{
				return (string)base["ExceptIfHeaderContainsMessageHeader"];
			}
			set
			{
				base["ExceptIfHeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000AAA24 File Offset: 0x000A8C24
		// (set) Token: 0x06003895 RID: 14485 RVA: 0x000AAA36 File Offset: 0x000A8C36
		[DataMember]
		public string[] ExceptIfHeaderContainsWords
		{
			get
			{
				return (string[])base["ExceptIfHeaderContainsWords"];
			}
			set
			{
				base["ExceptIfHeaderContainsWords"] = value;
			}
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x000AAA44 File Offset: 0x000A8C44
		// (set) Token: 0x06003897 RID: 14487 RVA: 0x000AAA56 File Offset: 0x000A8C56
		[DataMember]
		public string[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return (string[])base["ExceptIfFromAddressContainsWords"];
			}
			set
			{
				base["ExceptIfFromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x000AAA64 File Offset: 0x000A8C64
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x000AAA76 File Offset: 0x000A8C76
		[DataMember]
		public string[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return (string[])base["ExceptIfRecipientAddressContainsWords"];
			}
			set
			{
				base["ExceptIfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000AAA84 File Offset: 0x000A8C84
		// (set) Token: 0x0600389B RID: 14491 RVA: 0x000AAA96 File Offset: 0x000A8C96
		[DataMember]
		public string[] ExceptIfAnyOfRecipientAddressContainsWords
		{
			get
			{
				return (string[])base["ExceptIfAnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000AAAA4 File Offset: 0x000A8CA4
		// (set) Token: 0x0600389D RID: 14493 RVA: 0x000AAAB6 File Offset: 0x000A8CB6
		[DataMember]
		public string[] ExceptIfAttachmentContainsWords
		{
			get
			{
				return (string[])base["ExceptIfAttachmentContainsWords"];
			}
			set
			{
				base["ExceptIfAttachmentContainsWords"] = value;
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000AAAC4 File Offset: 0x000A8CC4
		// (set) Token: 0x0600389F RID: 14495 RVA: 0x000AAAD8 File Offset: 0x000A8CD8
		[DataMember]
		public bool? ExceptIfHasNoClassification
		{
			get
			{
				return (bool?)base["ExceptIfHasNoClassification"];
			}
			set
			{
				base["ExceptIfHasNoClassification"] = (value ?? false);
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000AAB0A File Offset: 0x000A8D0A
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000AAB1C File Offset: 0x000A8D1C
		[DataMember]
		public bool? ExceptIfAttachmentIsUnsupported
		{
			get
			{
				return (bool?)base["ExceptIfAttachmentIsUnsupported"];
			}
			set
			{
				base["ExceptIfAttachmentIsUnsupported"] = (value ?? false);
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x000AAB4E File Offset: 0x000A8D4E
		// (set) Token: 0x060038A3 RID: 14499 RVA: 0x000AAB60 File Offset: 0x000A8D60
		[DataMember]
		public string[] ExceptIfSenderADAttributeContainsWords
		{
			get
			{
				return (string[])base["ExceptIfSenderADAttributeContainsWords"];
			}
			set
			{
				base["ExceptIfSenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x000AAB6E File Offset: 0x000A8D6E
		// (set) Token: 0x060038A5 RID: 14501 RVA: 0x000AAB80 File Offset: 0x000A8D80
		[DataMember]
		public string[] ExceptIfRecipientADAttributeContainsWords
		{
			get
			{
				return (string[])base["ExceptIfRecipientADAttributeContainsWords"];
			}
			set
			{
				base["ExceptIfRecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x000AAB8E File Offset: 0x000A8D8E
		// (set) Token: 0x060038A7 RID: 14503 RVA: 0x000AABA0 File Offset: 0x000A8DA0
		[DataMember]
		public string[] ExceptIfRecipientDomainIs
		{
			get
			{
				return (string[])base["ExceptIfRecipientDomainIs"];
			}
			set
			{
				base["ExceptIfRecipientDomainIs"] = value;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x000AABAE File Offset: 0x000A8DAE
		// (set) Token: 0x060038A9 RID: 14505 RVA: 0x000AABC0 File Offset: 0x000A8DC0
		[DataMember]
		public string[] ExceptIfSenderDomainIs
		{
			get
			{
				return (string[])base["ExceptIfSenderDomainIs"];
			}
			set
			{
				base["ExceptIfSenderDomainIs"] = value;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x000AABCE File Offset: 0x000A8DCE
		// (set) Token: 0x060038AB RID: 14507 RVA: 0x000AABE0 File Offset: 0x000A8DE0
		[DataMember]
		public string[] ExceptIfContentCharacterSetContainsWords
		{
			get
			{
				return (string[])base["ExceptIfContentCharacterSetContainsWords"];
			}
			set
			{
				base["ExceptIfContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000AABEE File Offset: 0x000A8DEE
		// (set) Token: 0x060038AD RID: 14509 RVA: 0x000AAC00 File Offset: 0x000A8E00
		[DataMember]
		public string[] ExceptIfSubjectMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfSubjectMatchesPatterns"];
			}
			set
			{
				base["ExceptIfSubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x000AAC0E File Offset: 0x000A8E0E
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x000AAC20 File Offset: 0x000A8E20
		[DataMember]
		public string[] ExceptIfSubjectOrBodyMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfSubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000AAC2E File Offset: 0x000A8E2E
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000AAC40 File Offset: 0x000A8E40
		[DataMember]
		public string ExceptIfHeaderMatchesMessageHeader
		{
			get
			{
				return (string)base["ExceptIfHeaderMatchesMessageHeader"];
			}
			set
			{
				base["ExceptIfHeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000AAC4E File Offset: 0x000A8E4E
		// (set) Token: 0x060038B3 RID: 14515 RVA: 0x000AAC60 File Offset: 0x000A8E60
		[DataMember]
		public string[] ExceptIfHeaderMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfHeaderMatchesPatterns"];
			}
			set
			{
				base["ExceptIfHeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000AAC6E File Offset: 0x000A8E6E
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000AAC80 File Offset: 0x000A8E80
		[DataMember]
		public string[] ExceptIfFromAddressMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfFromAddressMatchesPatterns"];
			}
			set
			{
				base["ExceptIfFromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000AAC8E File Offset: 0x000A8E8E
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000AACA0 File Offset: 0x000A8EA0
		[DataMember]
		public string[] ExceptIfAttachmentNameMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfAttachmentNameMatchesPatterns"];
			}
			set
			{
				base["ExceptIfAttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x000AACAE File Offset: 0x000A8EAE
		// (set) Token: 0x060038B9 RID: 14521 RVA: 0x000AACC0 File Offset: 0x000A8EC0
		[DataMember]
		public string[] ExceptIfAttachmentMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfAttachmentMatchesPatterns"];
			}
			set
			{
				base["ExceptIfAttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000AACCE File Offset: 0x000A8ECE
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x000AACE0 File Offset: 0x000A8EE0
		[DataMember]
		public string[] ExceptIfRecipientAddressMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base["ExceptIfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000AACEE File Offset: 0x000A8EEE
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000AAD00 File Offset: 0x000A8F00
		[DataMember]
		public string[] ExceptIfAnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfAnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000AAD0E File Offset: 0x000A8F0E
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000AAD20 File Offset: 0x000A8F20
		[DataMember]
		public string[] ExceptIfSenderADAttributeMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfSenderADAttributeMatchesPatterns"];
			}
			set
			{
				base["ExceptIfSenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000AAD2E File Offset: 0x000A8F2E
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000AAD40 File Offset: 0x000A8F40
		[DataMember]
		public string[] ExceptIfRecipientADAttributeMatchesPatterns
		{
			get
			{
				return (string[])base["ExceptIfRecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000AAD4E File Offset: 0x000A8F4E
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000AAD60 File Offset: 0x000A8F60
		[DataMember]
		public string[] ExceptIfAttachmentExtensionMatchesWords
		{
			get
			{
				return (string[])base["ExceptIfAttachmentExtensionMatchesWords"];
			}
			set
			{
				base["ExceptIfAttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000AAD6E File Offset: 0x000A8F6E
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000AAD80 File Offset: 0x000A8F80
		[DataMember]
		public string[] ExceptIfSenderIpRanges
		{
			get
			{
				return (string[])base["ExceptIfSenderIpRanges"];
			}
			set
			{
				base["ExceptIfSenderIpRanges"] = value;
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x000AAD8E File Offset: 0x000A8F8E
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x000AADA0 File Offset: 0x000A8FA0
		[DataMember]
		public int? ExceptIfSCLOver
		{
			get
			{
				return (int?)base["ExceptIfSCLOver"];
			}
			set
			{
				base["ExceptIfSCLOver"] = value;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000AADB3 File Offset: 0x000A8FB3
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000AADC5 File Offset: 0x000A8FC5
		[DataMember]
		public long? ExceptIfAttachmentSizeOver
		{
			get
			{
				return (long?)base["ExceptIfAttachmentSizeOver"];
			}
			set
			{
				if (value != null)
				{
					base["ExceptIfAttachmentSizeOver"] = ByteQuantifiedSize.FromKB((ulong)value.Value);
					return;
				}
				base["ExceptIfAttachmentSizeOver"] = null;
			}
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000AADF9 File Offset: 0x000A8FF9
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x000AAE0C File Offset: 0x000A900C
		[DataMember]
		public bool? ExceptIfAttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool?)base["ExceptIfAttachmentProcessingLimitExceeded"];
			}
			set
			{
				base["ExceptIfAttachmentProcessingLimitExceeded"] = (value ?? false);
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000AAE3E File Offset: 0x000A903E
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x000AAE50 File Offset: 0x000A9050
		[DataMember]
		public long? ExceptIfMessageSizeOver
		{
			get
			{
				return (long?)base["ExceptIfMessageSizeOver"];
			}
			set
			{
				if (value != null)
				{
					base["ExceptIfMessageSizeOver"] = ByteQuantifiedSize.FromKB((ulong)value.Value);
					return;
				}
				base["ExceptIfMessageSizeOver"] = null;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000AAE84 File Offset: 0x000A9084
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x000AAE96 File Offset: 0x000A9096
		[DataMember]
		public Hashtable[] ExceptIfMessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base["ExceptIfMessageContainsDataClassifications"];
			}
			set
			{
				base["ExceptIfMessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000AAEA4 File Offset: 0x000A90A4
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000AAEB6 File Offset: 0x000A90B6
		[DataMember]
		public string ExceptIfWithImportance
		{
			get
			{
				return (string)base["ExceptIfWithImportance"];
			}
			set
			{
				base["ExceptIfWithImportance"] = value;
			}
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000AAEC4 File Offset: 0x000A90C4
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000AAED6 File Offset: 0x000A90D6
		[DataMember]
		public string ExceptIfMessageTypeMatches
		{
			get
			{
				return (string)base["ExceptIfMessageTypeMatches"];
			}
			set
			{
				base["ExceptIfMessageTypeMatches"] = value;
			}
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000AAEE4 File Offset: 0x000A90E4
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000AAEF8 File Offset: 0x000A90F8
		[DataMember]
		public bool? ExceptIfHasSenderOverride
		{
			get
			{
				return (bool?)base["ExceptIfHasSenderOverride"];
			}
			set
			{
				base["ExceptIfHasSenderOverride"] = (value ?? false);
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000AAF2A File Offset: 0x000A912A
		// (set) Token: 0x060038D7 RID: 14551 RVA: 0x000AAF3C File Offset: 0x000A913C
		[DataMember]
		public bool? ExceptIfAttachmentHasExecutableContent
		{
			get
			{
				return (bool?)base["ExceptIfAttachmentHasExecutableContent"];
			}
			set
			{
				base["ExceptIfAttachmentHasExecutableContent"] = (value ?? false);
			}
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000AAF6E File Offset: 0x000A916E
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000AAF80 File Offset: 0x000A9180
		[DataMember]
		public bool? ExceptIfAttachmentIsPasswordProtected
		{
			get
			{
				return (bool?)base["ExceptIfAttachmentIsPasswordProtected"];
			}
			set
			{
				base["ExceptIfAttachmentIsPasswordProtected"] = (value ?? false);
			}
		}
	}
}
