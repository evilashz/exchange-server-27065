using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000060 RID: 96
	internal class DialingPermissionsCheck
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		internal DialingPermissionsCheck(UMDialPlan originatingDialPlan, bool authenticatedCaller)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "DialingPermissionsCheck::ctor(DialPlan=({0}) authenticatedCaller=({1}))", new object[]
			{
				originatingDialPlan.Name,
				authenticatedCaller
			});
			this.dialPermissions = DialPermissionWrapper.CreateFromDialPlan(originatingDialPlan);
			this.authenticatedCaller = authenticatedCaller;
			this.originatingDialPlan = originatingDialPlan;
			this.operatorEnabled = PhoneNumber.TryParse(this.originatingDialPlan.OperatorExtension, out this.operatorNumber);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D360 File Offset: 0x0000B560
		internal DialingPermissionsCheck(UMAutoAttendant autoAttendant, AutoAttendantSettings currentAutoAttendantSettings, UMDialPlan originatingDialPlan)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "DialingPermissionsCheck::ctor(AA=({0}) Originating DP=({1}))", new object[]
			{
				autoAttendant.Name,
				originatingDialPlan.Name
			});
			this.dialPermissions = DialPermissionWrapper.CreateFromAutoAttendant(autoAttendant);
			this.originatingDialPlan = originatingDialPlan;
			this.operatorEnabled = CommonUtil.GetOperatorExtensionForAutoAttendant(autoAttendant, currentAutoAttendantSettings, originatingDialPlan, true, out this.operatorNumber);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
		internal DialingPermissionsCheck(ADUser user, UMDialPlan originatingDialPlan)
		{
			this.dialPermissions = DialPermissionWrapper.CreateFromRecipientPolicy(user);
			this.authenticatedCaller = true;
			this.originatingDialPlan = originatingDialPlan;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		public bool OperatorEnabled
		{
			get
			{
				return this.operatorEnabled;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000D3EF File Offset: 0x0000B5EF
		public PhoneNumber OperatorNumber
		{
			get
			{
				return this.operatorNumber;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		internal DialingPermissionsCheck.DialingPermissionsCheckResult CheckDirectoryUser(ADRecipient recipient, PhoneNumber phoneOrExtension)
		{
			DialingPermissionsCheck.DialingPermissionsCheckResult dialingPermissionsCheckResult = new DialingPermissionsCheck.DialingPermissionsCheckResult(this);
			dialingPermissionsCheckResult.CheckDirectoryUser(recipient, phoneOrExtension);
			return dialingPermissionsCheckResult;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D418 File Offset: 0x0000B618
		internal DialingPermissionsCheck.DialingPermissionsCheckResult CheckPhoneNumber(PhoneNumber number)
		{
			DialingPermissionsCheck.DialingPermissionsCheckResult dialingPermissionsCheckResult = new DialingPermissionsCheck.DialingPermissionsCheckResult(this);
			dialingPermissionsCheckResult.CheckPhoneNumber(number);
			return dialingPermissionsCheckResult;
		}

		// Token: 0x04000297 RID: 663
		private DialPermissionWrapper dialPermissions;

		// Token: 0x04000298 RID: 664
		private UMDialPlan originatingDialPlan;

		// Token: 0x04000299 RID: 665
		private bool authenticatedCaller;

		// Token: 0x0400029A RID: 666
		private bool operatorEnabled;

		// Token: 0x0400029B RID: 667
		private PhoneNumber operatorNumber;

		// Token: 0x02000061 RID: 97
		internal class DialingPermissionsCheckResult
		{
			// Token: 0x060003A7 RID: 935 RVA: 0x0000D434 File Offset: 0x0000B634
			internal DialingPermissionsCheckResult(DialingPermissionsCheck parent)
			{
				this.parent = parent;
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D443 File Offset: 0x0000B643
			public bool AllowCall
			{
				get
				{
					return this.canCall;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D44B File Offset: 0x0000B64B
			public bool AllowSendMessage
			{
				get
				{
					return this.canSendMsg;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D453 File Offset: 0x0000B653
			public bool HaveCallPermissions
			{
				get
				{
					return this.haveCallPermissions;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060003AB RID: 939 RVA: 0x0000D45B File Offset: 0x0000B65B
			public PhoneNumber NumberToDial
			{
				get
				{
					return this.numberToDial;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D463 File Offset: 0x0000B663
			public bool IsProtectedUser
			{
				get
				{
					return this.protectedUser;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D46B File Offset: 0x0000B66B
			// (set) Token: 0x060003AE RID: 942 RVA: 0x0000D473 File Offset: 0x0000B673
			public bool HaveValidPhone { get; private set; }

			// Token: 0x060003AF RID: 943 RVA: 0x0000D47C File Offset: 0x0000B67C
			internal void CheckDirectoryUser(ADRecipient recipient, PhoneNumber phoneOrExtension)
			{
				this.canCall = false;
				this.canSendMsg = false;
				this.haveCallPermissions = false;
				this.numberToDial = null;
				this.HaveValidPhone = false;
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, recipient.DisplayName);
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "CheckDirectoryUser(_UserDisplayName,\"{0}\").", new object[]
				{
					phoneOrExtension
				});
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(recipient);
				UMDialPlan dialPlanFromRecipient = iadsystemConfigurationLookup.GetDialPlanFromRecipient(recipient);
				if (phoneOrExtension == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "CheckDirectoryUser(_UserDisplayName) - getting GAL phone or UM extension.", new object[0]);
					this.HaveValidPhone = DialPermissions.GetBestOfficeNumber(recipient, this.parent.originatingDialPlan, out phoneOrExtension);
					CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "GetExtensionOrGalPhone(_UserDisplayName) returned Sucess:{0} Phone:{1}", new object[]
					{
						this.HaveValidPhone,
						(phoneOrExtension != null) ? phoneOrExtension.ToString() : "<null>"
					});
					if (this.HaveValidPhone && phoneOrExtension.Kind == PhoneNumberKind.Extension)
					{
						PhoneNumber value;
						if (DialPermissions.CheckExtension(phoneOrExtension, this.parent.dialPermissions, this.parent.originatingDialPlan, dialPlanFromRecipient, out value))
						{
							this.haveCallPermissions = true;
							this.numberToDial = value;
						}
						else
						{
							data = PIIMessage.Create(PIIType._UserDisplayName, (recipient != null) ? recipient.DisplayName : "<null>");
							CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "Extension {0} for _UserDisplayName did not pass dialing checks.", new object[]
							{
								phoneOrExtension
							});
						}
					}
				}
				else
				{
					this.HaveValidPhone = true;
				}
				if (this.HaveValidPhone)
				{
					PhoneNumber phoneNumber = DialPermissions.Canonicalize(phoneOrExtension, this.parent.originatingDialPlan, recipient, dialPlanFromRecipient);
					if (phoneNumber != null)
					{
						PIIMessage piimessage = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
						CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, piimessage, "Doing DialingPermission check for: PhoneNumber _PhoneNumber.", new object[0]);
						PhoneNumber value;
						this.haveCallPermissions = DialPermissions.Check(phoneNumber, this.parent.dialPermissions, this.parent.originatingDialPlan, dialPlanFromRecipient, out value);
						PIIMessage piimessage2 = PIIMessage.Create(PIIType._Callee, value);
						PIIMessage[] data2 = new PIIMessage[]
						{
							piimessage,
							piimessage2
						};
						CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data2, "DialingPermission check for: _PhoneNumber returned result,phone = {0}[_Callee].", new object[]
						{
							this.haveCallPermissions
						});
						if (this.haveCallPermissions)
						{
							this.numberToDial = value;
						}
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, "PhoneNumber {0} did not pass canonicalization.", new object[]
						{
							phoneOrExtension
						});
					}
				}
				if (!this.parent.dialPermissions.CallSomeoneEnabled)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, "DialPermissionWrapper.CallSomeoneEnabled = false. Hence setting haveCallPermissions = false.", new object[0]);
					this.haveCallPermissions = false;
				}
				if (!this.parent.authenticatedCaller)
				{
					if (this.haveCallPermissions)
					{
						this.ProcessAnonymousCaller(recipient);
					}
				}
				else
				{
					this.canCall = this.haveCallPermissions;
				}
				bool flag = recipient.PrimarySmtpAddress != SmtpAddress.Empty;
				if (flag)
				{
					this.canSendMsg = (this.parent.dialPermissions.SendVoiceMessageEnabled || !this.HaveValidPhone || !this.haveCallPermissions);
				}
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x0000D768 File Offset: 0x0000B968
			internal void CheckPhoneNumber(PhoneNumber number)
			{
				PIIMessage piimessage = PIIMessage.Create(PIIType._PhoneNumber, number);
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, piimessage, "CheckPhoneNumber(_PhoneNumber).", new object[0]);
				PhoneNumber phoneNumber = DialPermissions.Canonicalize(number, this.parent.originatingDialPlan, null);
				PIIMessage piimessage2 = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
				PIIMessage[] data = new PIIMessage[]
				{
					piimessage,
					piimessage2
				};
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "Canonicalize(_PhoneNumber(1)) returned {_PhoneNumber(2)}.", new object[0]);
				if (phoneNumber == null)
				{
					return;
				}
				PhoneNumber phoneNumber2 = null;
				this.haveCallPermissions = DialPermissions.Check(phoneNumber, this.parent.dialPermissions, this.parent.originatingDialPlan, null, out phoneNumber2);
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, piimessage2, "CheckPermissions(_PhoneNumber) returned {0}.", new object[]
				{
					this.haveCallPermissions
				});
				if (this.haveCallPermissions)
				{
					this.canCall = true;
					this.numberToDial = phoneNumber2;
				}
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x0000D84C File Offset: 0x0000BA4C
			internal void ProcessAnonymousCaller(ADRecipient recipient)
			{
				if ((recipient.AllowUMCallsFromNonUsers & AllowUMCallsFromNonUsersFlags.SearchEnabled) == AllowUMCallsFromNonUsersFlags.SearchEnabled)
				{
					this.canCall = true;
					return;
				}
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, recipient.DisplayName);
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "Recipient _UserDisplayName does not allow calls from nonUMusers.", new object[0]);
				this.protectedUser = true;
				if (this.parent.operatorEnabled)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, data, "ProcessAnonymousCaller(_UserDisplayName) returned operator number {0}.", new object[]
					{
						this.parent.operatorNumber
					});
					this.numberToDial = this.parent.operatorNumber;
					this.canCall = true;
					return;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.OutdialingTracer, this, "Operator is not configured for AA, and recipient does not allow calls from NonUMEnabled users.", new object[0]);
				this.canCall = false;
			}

			// Token: 0x0400029C RID: 668
			private DialingPermissionsCheck parent;

			// Token: 0x0400029D RID: 669
			private bool canCall;

			// Token: 0x0400029E RID: 670
			private bool canSendMsg;

			// Token: 0x0400029F RID: 671
			private bool haveCallPermissions;

			// Token: 0x040002A0 RID: 672
			private PhoneNumber numberToDial;

			// Token: 0x040002A1 RID: 673
			private bool protectedUser;
		}
	}
}
