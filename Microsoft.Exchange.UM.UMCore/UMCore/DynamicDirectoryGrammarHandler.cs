﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200026C RID: 620
	internal class DynamicDirectoryGrammarHandler : DirectoryGrammarHandler
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x00051270 File Offset: 0x0004F470
		public DynamicDirectoryGrammarHandler(OrganizationId orgId) : base(orgId)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Creating dynamic directory grammar handler for org '{0}'", new object[]
			{
				base.OrgId
			});
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000512A5 File Offset: 0x0004F4A5
		public override bool DeleteFileAfterUse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000512A8 File Offset: 0x0004F4A8
		public override string ToString()
		{
			return base.OrgId.ToString();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000512B8 File Offset: 0x0004F4B8
		public override void PrepareGrammarAsync(CallContext callContext, DirectoryGrammarHandler.GrammarType grammarType)
		{
			ValidateArgument.NotNull(callContext, "callContext");
			ITempFile tempFile = TempFileFactory.CreateTempFile();
			string filePath = tempFile.FilePath;
			CultureInfo culture = callContext.Culture;
			switch (grammarType)
			{
			case DirectoryGrammarHandler.GrammarType.User:
			{
				DirectoryGrammar directoryGrammar = this.GetUserDirectoryGrammar(callContext);
				directoryGrammar.InitializeGrammar(filePath, culture);
				this.AddRecipientEntries(directoryGrammar, GrammarRecipientHelper.GetUserFilter());
				directoryGrammar.CompleteGrammar();
				this.searchGrammarFile = new GalGrammarFile(culture, filePath);
				return;
			}
			case DirectoryGrammarHandler.GrammarType.DL:
			{
				DirectoryGrammar directoryGrammar = new DistributionListGrammar();
				directoryGrammar.InitializeGrammar(filePath, culture);
				this.AddRecipientEntries(directoryGrammar, GrammarRecipientHelper.GetDLFilter());
				directoryGrammar.CompleteGrammar();
				this.searchGrammarFile = new DistributionListGrammarFile(culture, filePath);
				return;
			}
			default:
				ExAssert.RetailAssert(false, "Unknown grammar type {0}", new object[]
				{
					grammarType
				});
				return;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00051378 File Offset: 0x0004F578
		public override void PrepareGrammarAsync(ADRecipient recipient, CultureInfo culture)
		{
			ValidateArgument.NotNull(recipient, "recipient");
			ValidateArgument.NotNull(culture, "culture");
			Exception ex = null;
			try
			{
				ITempFile tempFile = TempFileFactory.CreateTempFile();
				string filePath = tempFile.FilePath;
				DirectoryGrammar directoryGrammar = new GalUserGrammar();
				directoryGrammar.InitializeGrammar(filePath, culture);
				this.AddRecipientEntries(directoryGrammar, GrammarRecipientHelper.GetUserFilter());
				directoryGrammar.CompleteGrammar();
				this.searchGrammarFile = new GalGrammarFile(culture, filePath);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADOperationException ex3)
			{
				ex = ex3;
			}
			catch (DataValidationException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					CallIdTracer.TraceError(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Error dynamically generating grammar for org '{0}': '{1}'", new object[]
					{
						base.OrgId,
						ex
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DynamicDirectoryGrammarGenerationFailure, null, new object[]
					{
						base.OrgId,
						CommonUtil.ToEventLogString(ex)
					});
					UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
					this.searchGrammarFile = null;
				}
				else
				{
					UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
				}
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000514B8 File Offset: 0x0004F6B8
		public override SearchGrammarFile WaitForPrepareGrammarCompletion()
		{
			return this.searchGrammarFile;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00051768 File Offset: 0x0004F968
		private void AddRecipientEntries(DirectoryGrammar grammar, QueryFilter queryFilter)
		{
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(base.OrgId, null);
			iadrecipientLookup.ProcessRecipients(queryFilter, GrammarRecipientHelper.LookupProperties, delegate(ADRawEntry rawEntry)
			{
				List<string> list = new List<string>(1);
				string smtpAddress = null;
				Guid objectGuid = Guid.Empty;
				RecipientType recipientType = (RecipientType)(-1);
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)(-1L);
				Guid dialPlanGuid = Guid.Empty;
				List<Guid> list2 = new List<Guid>();
				object[] properties = rawEntry.GetProperties(GrammarRecipientHelper.LookupProperties);
				int i = 0;
				while (i < properties.Length)
				{
					string empty = string.Empty;
					if (properties[i] == null)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "ADEntry - Property='{0}', Value is null", new object[]
						{
							GrammarRecipientHelper.LookupProperties[i].Name
						});
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.UMRecipientDialPlanId)
					{
						ADObjectId adobjectId = properties[i] as ADObjectId;
						dialPlanGuid = adobjectId.ObjectGuid;
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.AddressListMembership)
					{
						ADMultiValuedProperty<ADObjectId> admultiValuedProperty = properties[i] as ADMultiValuedProperty<ADObjectId>;
						using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = admultiValuedProperty.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ADObjectId adobjectId2 = enumerator.Current;
								list2.Add(adobjectId2.ObjectGuid);
							}
							goto IL_1F9;
						}
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.DisplayName || GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.PhoneticDisplayName)
					{
						list.Add(properties[i].ToString());
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.PrimarySmtpAddress)
					{
						smtpAddress = GrammarRecipientHelper.GetNormalizedEmailAddress(properties[i].ToString());
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADObjectSchema.Guid)
					{
						objectGuid = (Guid)properties[i];
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.RecipientType)
					{
						recipientType = (RecipientType)properties[i];
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.RecipientTypeDetails)
					{
						recipientTypeDetails = (RecipientTypeDetails)properties[i];
						goto IL_1F9;
					}
					if (GrammarRecipientHelper.LookupProperties[i] != ADObjectSchema.DistinguishedName && GrammarRecipientHelper.LookupProperties[i] != ADObjectSchema.WhenChangedUTC)
					{
						ExAssert.RetailAssert(false, "Invalid lookup property '{0}'", new object[]
						{
							GrammarRecipientHelper.LookupProperties[i].Name
						});
						goto IL_1F9;
					}
					IL_238:
					i++;
					continue;
					IL_1F9:
					CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "ADEntry -  Property='{0}', Value='{1}'", new object[]
					{
						GrammarRecipientHelper.LookupProperties[i].Name,
						empty ?? "<null>"
					});
					goto IL_238;
				}
				list = this.ProcessName(list, recipientType);
				if (recipientTypeDetails != RecipientTypeDetails.MailboxPlan && list != null)
				{
					ADEntry entry = new ADEntry(list, smtpAddress, objectGuid, recipientType, dialPlanGuid, list2);
					grammar.WriteADEntry(entry);
				}
			}, 3);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000517B0 File Offset: 0x0004F9B0
		private List<string> ProcessName(List<string> names, RecipientType recipientType)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < names.Count; i++)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Processing name='{0}'", new object[]
				{
					names[i]
				});
				string sanitizedDisplayNameForXMLEntry = GrammarRecipientHelper.GetSanitizedDisplayNameForXMLEntry(names[i]);
				if (!string.IsNullOrEmpty(sanitizedDisplayNameForXMLEntry))
				{
					string input = GrammarRecipientHelper.CharacterMapReplaceString(sanitizedDisplayNameForXMLEntry);
					Dictionary<string, bool> dictionary = GrammarRecipientHelper.ApplyExclusionList(input, recipientType);
					if (dictionary.Count != 0)
					{
						foreach (string text in dictionary.Keys)
						{
							if (!string.IsNullOrEmpty(text) && this.IsGrammarEntryFormatValid(text))
							{
								string text2 = SpeechUtils.SrgsEncode(text);
								if (!string.IsNullOrEmpty(text2))
								{
									CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Adding name='{0}'", new object[]
									{
										text2
									});
									list.Add(text2);
								}
							}
						}
					}
				}
			}
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000518CC File Offset: 0x0004FACC
		private DirectoryGrammar GetUserDirectoryGrammar(CallContext callContext)
		{
			DirectoryGrammar result = null;
			switch (callContext.CallType)
			{
			case 1:
			{
				UMDialPlan dialPlan = callContext.DialPlan;
				switch (dialPlan.ContactScope)
				{
				case CallSomeoneScopeEnum.DialPlan:
					result = new DialPlanGrammar(dialPlan.Guid);
					break;
				case CallSomeoneScopeEnum.GlobalAddressList:
					result = new GalUserGrammar();
					break;
				case CallSomeoneScopeEnum.AddressList:
					if (dialPlan.ContactAddressList != null)
					{
						throw new NotSupportedException();
					}
					result = new GalUserGrammar();
					break;
				}
				break;
			}
			case 2:
			{
				UMAutoAttendant autoAttendantInfo = callContext.AutoAttendantInfo;
				switch (autoAttendantInfo.ContactScope)
				{
				case DialScopeEnum.DialPlan:
					result = new DialPlanGrammar(autoAttendantInfo.UMDialPlan.ObjectGuid);
					break;
				case DialScopeEnum.GlobalAddressList:
					result = new GalUserGrammar();
					break;
				case DialScopeEnum.AddressList:
					if (autoAttendantInfo.ContactAddressList != null)
					{
						throw new NotSupportedException();
					}
					result = new GalUserGrammar();
					break;
				}
				break;
			}
			case 3:
			{
				UMSubscriber callerInfo = callContext.CallerInfo;
				ADRecipient adrecipient = callerInfo.ADRecipient;
				ExAssert.RetailAssert(adrecipient != null, "subscriber.ADRecipient = null");
				ExAssert.RetailAssert(adrecipient.OrganizationId != null, "subscriber.ADRecipient.OrganizationId = null");
				if (adrecipient.AddressBookPolicy != null && adrecipient.GlobalAddressListFromAddressBookPolicy != null)
				{
					result = new AddressListGrammar(adrecipient.GlobalAddressListFromAddressBookPolicy.ObjectGuid);
				}
				else
				{
					result = new GalUserGrammar();
				}
				break;
			}
			default:
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unhandled Call Type {0}", new object[]
				{
					callContext.CallType
				}));
			}
			return result;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00051A50 File Offset: 0x0004FC50
		private bool IsGrammarEntryFormatValid(string wordToCheck)
		{
			bool result;
			try
			{
				Platform.Utilities.CheckGrammarEntryFormat(wordToCheck);
				result = true;
			}
			catch (FormatException)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Format exception for word='{0}'", new object[]
				{
					wordToCheck ?? "<null>"
				});
				result = false;
			}
			return result;
		}

		// Token: 0x04000C13 RID: 3091
		private const int MaxTransientExceptionRetries = 3;

		// Token: 0x04000C14 RID: 3092
		private SearchGrammarFile searchGrammarFile;
	}
}
