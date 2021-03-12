using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E5 RID: 1765
	internal class CASMailboxHelper
	{
		// Token: 0x060051F0 RID: 20976 RVA: 0x0012E358 File Offset: 0x0012C558
		private static string GetSettingsString(IPropertyBag propertyBag, string protocolName, int position, string defaultValue)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ProtocolSettings];
			foreach (string text in multiValuedProperty)
			{
				if (text.StartsWith(protocolName, StringComparison.OrdinalIgnoreCase))
				{
					string[] array = text.Split(new char[]
					{
						'§'
					});
					if (array.Length <= position || string.IsNullOrEmpty(array[position]))
					{
						return defaultValue;
					}
					return array[position];
				}
			}
			return defaultValue;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0012E3F4 File Offset: 0x0012C5F4
		private static string GetSettingsString(IPropertyBag propertyBag, string protocolName, int position, ADPropertyDefinition propertyDefinition)
		{
			return CASMailboxHelper.GetSettingsString(propertyBag, protocolName, position, (string)propertyDefinition.DefaultValue);
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0012E40C File Offset: 0x0012C60C
		private static bool GetSettingsBool(IPropertyBag propertyBag, string protocolName, int position, ADPropertyDefinition propertyDefinition)
		{
			string defaultValue = ((bool)propertyDefinition.DefaultValue) ? "1" : "0";
			string settingsString = CASMailboxHelper.GetSettingsString(propertyBag, protocolName, position, defaultValue);
			return string.Compare(settingsString, "1", StringComparison.OrdinalIgnoreCase) == 0 || (string.Compare(settingsString, "0", StringComparison.OrdinalIgnoreCase) != 0 && (bool)propertyDefinition.DefaultValue);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0012E468 File Offset: 0x0012C668
		private static int GetSettingsInt(IPropertyBag propertyBag, string protocolName, int position, ADPropertyDefinition propertyDefinition)
		{
			int result = (propertyDefinition.DefaultValue != null) ? ((int)propertyDefinition.DefaultValue) : 0;
			string settingsString = CASMailboxHelper.GetSettingsString(propertyBag, protocolName, position, result.ToString());
			int result2;
			if (!int.TryParse(settingsString, out result2))
			{
				return result;
			}
			return result2;
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0012E4A9 File Offset: 0x0012C6A9
		private static MimeTextFormat GetSettingsMimeTextFormat(IPropertyBag propertyBag, string protocolName, int position, ADPropertyDefinition propertyDefinition)
		{
			return (MimeTextFormat)CASMailboxHelper.GetSettingsInt(propertyBag, protocolName, position, propertyDefinition);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0012E4B4 File Offset: 0x0012C6B4
		private static bool? EwsGetTupleBool(IPropertyBag propertyBag, string wellKnownApplicationName)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[CASMailboxSchema.EwsWellKnownApplicationAccessPolicies];
			foreach (string text in multiValuedProperty)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				if (array.Length != 2)
				{
					ExTraceGlobals.GetCASMailboxTracer.TraceDebug<string>(0L, "Get-CASMailbox: policy must be in Allow:Application/Block:Application form. Skipping policy '{0}'.", text);
				}
				else if (string.Compare(array[1], wellKnownApplicationName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.Compare(array[0], "Allow", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new bool?(true);
					}
					if (string.Compare(array[0], "Block", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new bool?(false);
					}
					ExTraceGlobals.GetCASMailboxTracer.TraceDebug<string>(0L, "Get-CASMailbox: policy must be in Allow:Application/Block:Application form. Skipping policy '{0}'.", text);
				}
			}
			return null;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0012E5A0 File Offset: 0x0012C7A0
		private static void SetSettingsString(IPropertyBag propertyBag, string protocolName, int position, string value, int totalNumberOfFields)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.ProtocolSettings];
			string[] array = null;
			foreach (string text in multiValuedProperty)
			{
				if (text.StartsWith(protocolName, StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty.Remove(text);
					array = text.Split(new char[]
					{
						'§'
					});
					break;
				}
			}
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				if (multiValuedProperty[i].StartsWith(protocolName, StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty.RemoveAt(i);
					i--;
				}
			}
			if (array == null)
			{
				array = new string[totalNumberOfFields];
				array[0] = protocolName;
			}
			else if (array.Length < totalNumberOfFields)
			{
				Array.Resize<string>(ref array, totalNumberOfFields);
			}
			array[position] = value;
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < array.Length; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append('§');
				}
				if (!string.IsNullOrEmpty(array[j]))
				{
					stringBuilder.Append(array[j]);
				}
			}
			multiValuedProperty.Add(stringBuilder.ToString());
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0012E6CC File Offset: 0x0012C8CC
		private static void SetSettingsBool(IPropertyBag propertyBag, string protocolName, int position, bool value, int totalNumberOfFields)
		{
			string value2 = value ? "1" : "0";
			CASMailboxHelper.SetSettingsString(propertyBag, protocolName, position, value2, totalNumberOfFields);
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0012E6F4 File Offset: 0x0012C8F4
		private static void SetSettingsInt(IPropertyBag propertyBag, string protocolName, int position, int intValue, int totalNumberOfFields)
		{
			string value = intValue.ToString(CultureInfo.InvariantCulture);
			CASMailboxHelper.SetSettingsString(propertyBag, protocolName, position, value, totalNumberOfFields);
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x0012E719 File Offset: 0x0012C919
		private static void SetSettingsMimeTextFormat(IPropertyBag propertyBag, string protocolName, int position, MimeTextFormat value, int totalNumberOfFields)
		{
			CASMailboxHelper.SetSettingsInt(propertyBag, protocolName, position, (int)value, totalNumberOfFields);
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0012E728 File Offset: 0x0012C928
		private static void EwsSetTupleBool(IPropertyBag propertyBag, string wellKnownApplicationName, bool? allow)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[CASMailboxSchema.EwsWellKnownApplicationAccessPolicies];
			foreach (string text in multiValuedProperty)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				if (string.Compare(array[1], wellKnownApplicationName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					multiValuedProperty.Remove(text);
					break;
				}
			}
			if (allow != null)
			{
				string arg = (allow == true) ? "Allow" : "Block";
				multiValuedProperty.Add(string.Format("{0}:{1}", arg, wellKnownApplicationName));
			}
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0012E808 File Offset: 0x0012CA08
		internal static GetterDelegate RemotePowerShellEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "RemotePowerShell", 1, ADRecipientSchema.RemotePowerShellEnabled);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0012E84A File Offset: 0x0012CA4A
		internal static SetterDelegate RemotePowerShellEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				value = ((value == null) ? false : value);
				CASMailboxHelper.SetSettingsBool(propertyBag, "RemotePowerShell", 1, (bool)value, 2);
			};
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0012E881 File Offset: 0x0012CA81
		internal static GetterDelegate ECPEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "ECP", 1, ADRecipientSchema.ECPEnabled);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0012E8B5 File Offset: 0x0012CAB5
		internal static SetterDelegate ECPEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "ECP", 1, (bool)value, 2);
			};
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0012E8EC File Offset: 0x0012CAEC
		internal static GetterDelegate PopEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "POP3", 1, ADRecipientSchema.PopEnabled);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0012E921 File Offset: 0x0012CB21
		internal static SetterDelegate PopEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "POP3", 1, (bool)value, 14);
			};
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x0012E958 File Offset: 0x0012CB58
		internal static GetterDelegate PopUseProtocolDefaultsGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "POP3", 2, ADRecipientSchema.PopUseProtocolDefaults);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0012E98D File Offset: 0x0012CB8D
		internal static SetterDelegate PopUseProtocolDefaultsSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "POP3", 2, (bool)value, 14);
			};
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0012E9C5 File Offset: 0x0012CBC5
		internal static GetterDelegate PopMessagesRetrievalMimeFormatGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsMimeTextFormat(propertyBag, "POP3", 9, ADRecipientSchema.PopMessagesRetrievalMimeFormat);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0012E9FB File Offset: 0x0012CBFB
		internal static SetterDelegate PopMessagesRetrievalMimeFormatSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsMimeTextFormat(propertyBag, "POP3", 9, (MimeTextFormat)value, 14);
			};
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0012EA33 File Offset: 0x0012CC33
		internal static GetterDelegate PopEnableExactRFC822SizeGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "POP3", 10, ADRecipientSchema.PopEnableExactRFC822Size);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x0012EA69 File Offset: 0x0012CC69
		internal static SetterDelegate PopEnableExactRFC822SizeSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "POP3", 10, (bool)value, 14);
			};
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x0012EAA1 File Offset: 0x0012CCA1
		internal static GetterDelegate PopProtocolLoggingEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsInt(propertyBag, "POP3", 11, ADRecipientSchema.PopProtocolLoggingEnabled);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0012EAEF File Offset: 0x0012CCEF
		internal static SetterDelegate PopProtocolLoggingEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "POP3", 11, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsInt(propertyBag, "POP3", 11, (int)value, 14);
			};
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0012EB27 File Offset: 0x0012CD27
		internal static GetterDelegate PopSuppressReadReceiptGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "POP3", 12, ADRecipientSchema.PopSuppressReadReceipt);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0012EB75 File Offset: 0x0012CD75
		internal static SetterDelegate PopSuppressReadReceiptSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "POP3", 12, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsBool(propertyBag, "POP3", 12, (bool)value, 14);
			};
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x0012EBAD File Offset: 0x0012CDAD
		internal static GetterDelegate PopForceICalForCalendarRetrievalOptionGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "POP3", 13, ADRecipientSchema.PopForceICalForCalendarRetrievalOption);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0012EBFB File Offset: 0x0012CDFB
		internal static SetterDelegate PopForceICalForCalendarRetrievalOptionSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "POP3", 13, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsBool(propertyBag, "POP3", 13, (bool)value, 14);
			};
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0012EC32 File Offset: 0x0012CE32
		internal static GetterDelegate ImapEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "IMAP4", 1, ADRecipientSchema.ImapEnabled);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0012EC67 File Offset: 0x0012CE67
		internal static SetterDelegate ImapEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "IMAP4", 1, (bool)value, 14);
			};
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0012EC9E File Offset: 0x0012CE9E
		internal static GetterDelegate ImapUseProtocolDefaultsGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "IMAP4", 2, ADRecipientSchema.ImapUseProtocolDefaults);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0012ECD3 File Offset: 0x0012CED3
		internal static SetterDelegate ImapUseProtocolDefaultsSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "IMAP4", 2, (bool)value, 14);
			};
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x0012ED0B File Offset: 0x0012CF0B
		internal static GetterDelegate ImapMessagesRetrievalMimeFormatGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsMimeTextFormat(propertyBag, "IMAP4", 9, ADRecipientSchema.ImapMessagesRetrievalMimeFormat);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0012ED41 File Offset: 0x0012CF41
		internal static SetterDelegate ImapMessagesRetrievalMimeFormatSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsMimeTextFormat(propertyBag, "IMAP4", 9, (MimeTextFormat)value, 14);
			};
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0012ED79 File Offset: 0x0012CF79
		internal static GetterDelegate ImapEnableExactRFC822SizeGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "IMAP4", 10, ADRecipientSchema.ImapEnableExactRFC822Size);
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x0012EDAF File Offset: 0x0012CFAF
		internal static SetterDelegate ImapEnableExactRFC822SizeSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "IMAP4", 10, (bool)value, 14);
			};
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0012EDE7 File Offset: 0x0012CFE7
		internal static GetterDelegate ImapProtocolLoggingEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsInt(propertyBag, "IMAP4", 11, ADRecipientSchema.ImapProtocolLoggingEnabled);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0012EE35 File Offset: 0x0012D035
		internal static SetterDelegate ImapProtocolLoggingEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "IMAP4", 11, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsInt(propertyBag, "IMAP4", 11, (int)value, 14);
			};
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0012EE6D File Offset: 0x0012D06D
		internal static GetterDelegate ImapSuppressReadReceiptGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "IMAP4", 12, ADRecipientSchema.ImapSuppressReadReceipt);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x0012EEBB File Offset: 0x0012D0BB
		internal static SetterDelegate ImapSuppressReadReceiptSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "IMAP4", 12, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsBool(propertyBag, "IMAP4", 12, (bool)value, 14);
			};
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0012EEF3 File Offset: 0x0012D0F3
		internal static GetterDelegate ImapForceICalForCalendarRetrievalOptionGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "IMAP4", 13, ADRecipientSchema.ImapForceICalForCalendarRetrievalOption);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0012EF41 File Offset: 0x0012D141
		internal static SetterDelegate ImapForceICalForCalendarRetrievalOptionSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				if (value == null)
				{
					CASMailboxHelper.SetSettingsString(propertyBag, "IMAP4", 13, string.Empty, 14);
					return;
				}
				CASMailboxHelper.SetSettingsBool(propertyBag, "IMAP4", 13, (bool)value, 14);
			};
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0012EF78 File Offset: 0x0012D178
		internal static GetterDelegate MAPIEnabledGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "MAPI", 1, ADRecipientSchema.MAPIEnabled);
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0012EFAD File Offset: 0x0012D1AD
		internal static SetterDelegate MAPIEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "MAPI", 1, (bool)value, 11);
			};
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0012F010 File Offset: 0x0012D210
		internal static GetterDelegate MapiHttpEnabledGetterDelegate()
		{
			return delegate(IPropertyBag propertyBag)
			{
				string settingsString = CASMailboxHelper.GetSettingsString(propertyBag, "MAPI", 9, "U");
				if (settingsString == "U")
				{
					return null;
				}
				return new bool?(settingsString == "Y");
			};
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0012F073 File Offset: 0x0012D273
		internal static SetterDelegate MapiHttpEnabledSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				string value2 = "U";
				if (value != null)
				{
					value2 = (((bool?)value).Value ? "Y" : "N");
				}
				CASMailboxHelper.SetSettingsString(propertyBag, "MAPI", 9, value2, 11);
			};
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0012F0AA File Offset: 0x0012D2AA
		internal static GetterDelegate MAPIBlockOutlookNonCachedModeGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "MAPI", 2, ADRecipientSchema.MAPIBlockOutlookNonCachedMode);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0012F0DF File Offset: 0x0012D2DF
		internal static SetterDelegate MAPIBlockOutlookNonCachedModeSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "MAPI", 2, (bool)value, 11);
			};
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0012F111 File Offset: 0x0012D311
		internal static GetterDelegate MAPIBlockOutlookVersionsGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsString(propertyBag, "MAPI", 4, ADRecipientSchema.MAPIBlockOutlookVersions);
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0012F146 File Offset: 0x0012D346
		internal static SetterDelegate MAPIBlockOutlookVersionsSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsString(propertyBag, "MAPI", 4, (string)value, 11);
			};
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0012F17D File Offset: 0x0012D37D
		internal static GetterDelegate MAPIBlockOutlookRpcHttpGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "MAPI", 5, ADRecipientSchema.MAPIBlockOutlookRpcHttp);
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0012F1B2 File Offset: 0x0012D3B2
		internal static SetterDelegate MAPIBlockOutlookRpcHttpSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "MAPI", 5, (bool)value, 11);
			};
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0012F1EA File Offset: 0x0012D3EA
		internal static GetterDelegate MAPIBlockOutlookExternalConnectivityGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.GetSettingsBool(propertyBag, "MAPI", 10, ADRecipientSchema.MAPIBlockOutlookExternalConnectivity);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0012F220 File Offset: 0x0012D420
		internal static SetterDelegate MAPIBlockOutlookExternalConnectivitySetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.SetSettingsBool(propertyBag, "MAPI", 10, (bool)value, 11);
			};
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0012F251 File Offset: 0x0012D451
		internal static GetterDelegate EwsOutlookAccessPoliciesGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.EwsGetTupleBool(propertyBag, "Outlook");
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0012F283 File Offset: 0x0012D483
		internal static SetterDelegate EwsOutlookAccessPoliciesSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.EwsSetTupleBool(propertyBag, "Outlook", (bool?)value);
			};
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0012F2B4 File Offset: 0x0012D4B4
		internal static GetterDelegate EwsMacOutlookAccessPoliciesGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.EwsGetTupleBool(propertyBag, "MacOutlook");
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x0012F2E6 File Offset: 0x0012D4E6
		internal static SetterDelegate EwsMacOutlookAccessPoliciesSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.EwsSetTupleBool(propertyBag, "MacOutlook", (bool?)value);
			};
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0012F317 File Offset: 0x0012D517
		internal static GetterDelegate EwsEntourageAccessPoliciesGetterDelegate()
		{
			return (IPropertyBag propertyBag) => CASMailboxHelper.EwsGetTupleBool(propertyBag, "Entourage");
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0012F349 File Offset: 0x0012D549
		internal static SetterDelegate EwsEntourageAccessPoliciesSetterDelegate()
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				CASMailboxHelper.EwsSetTupleBool(propertyBag, "Entourage", (bool?)value);
			};
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0012F368 File Offset: 0x0012D568
		internal static bool? ToBooleanNullable(int? value)
		{
			if (value == null)
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(value.Value));
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0012F39C File Offset: 0x0012D59C
		internal static int? ToInt32Nullable(bool? value)
		{
			if (value == null)
			{
				return null;
			}
			return new int?(Convert.ToInt32(value.Value));
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0012F3D0 File Offset: 0x0012D5D0
		internal static int? ToInt32Nullable(EwsApplicationAccessPolicy? value)
		{
			if (value == null)
			{
				return null;
			}
			return new int?((int)value.Value);
		}

		// Token: 0x04003772 RID: 14194
		private const int IdxPopImapEnabled = 1;

		// Token: 0x04003773 RID: 14195
		private const int IdxPopImapUseProtocolDefaults = 2;

		// Token: 0x04003774 RID: 14196
		private const int IdxPopImapMessagesRetrievalMimeFormat = 9;

		// Token: 0x04003775 RID: 14197
		private const int IdxPopImapEnableExactRFC822Size = 10;

		// Token: 0x04003776 RID: 14198
		private const int IdxPopImapProtocolLoggingEnabled = 11;

		// Token: 0x04003777 RID: 14199
		private const int IdxPopImapSuppressReadReceipt = 12;

		// Token: 0x04003778 RID: 14200
		private const int IdxPopImapForceICalForCalendarRetrievalOption = 13;

		// Token: 0x04003779 RID: 14201
		private const int PopImapTotalNumberOfFields = 14;

		// Token: 0x0400377A RID: 14202
		private const int IdxMAPIEnabled = 1;

		// Token: 0x0400377B RID: 14203
		private const int IdxMAPIBlockOutlookNonCachedMode = 2;

		// Token: 0x0400377C RID: 14204
		private const int IdxMAPIBlockOutlookVersions = 4;

		// Token: 0x0400377D RID: 14205
		private const int IdxMAPIBlockOutlookRpcHttp = 5;

		// Token: 0x0400377E RID: 14206
		private const int IdxMapiHttpEnabled = 9;

		// Token: 0x0400377F RID: 14207
		private const int IdxMAPIBlockOutlookExternalConnectivity = 10;

		// Token: 0x04003780 RID: 14208
		private const int MAPIProtocolSettingTotalNumberOfFields = 11;

		// Token: 0x04003781 RID: 14209
		private const char SectionSymbol = '§';

		// Token: 0x04003782 RID: 14210
		private const string OutlookName = "Outlook";

		// Token: 0x04003783 RID: 14211
		private const string MacOutlookName = "MacOutlook";

		// Token: 0x04003784 RID: 14212
		private const string EntourageName = "Entourage";

		// Token: 0x04003785 RID: 14213
		internal static string MAPIBlockOutlookVersionsPattern = "^((((((\\d+(\\.\\d+){2})?\\-(\\d+(\\.\\d+){2})?)|(\\d+(\\.\\d+){2}))([,;] ?(((\\d+(\\.\\d+){2})?\\-(\\d+(\\.\\d+){2})?)|(\\d+(\\.\\d+){2})))*)?)|([0]))$";
	}
}
