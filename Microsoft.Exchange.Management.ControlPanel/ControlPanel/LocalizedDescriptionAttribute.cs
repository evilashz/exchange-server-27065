using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel.EnumTypes;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tracking;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200055B RID: 1371
	public class LocalizedDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000C2DA9 File Offset: 0x000C0FA9
		public static ICollection EnumTypeKeys
		{
			get
			{
				return LocalizedDescriptionAttribute.enumType2ResourceManagerTable.Keys;
			}
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x000C2DB5 File Offset: 0x000C0FB5
		public static ICollection EnumTypeKeysInOwaOption
		{
			get
			{
				return LocalizedDescriptionAttribute.enumType2ResourceManagerTableForOwaOption.Keys;
			}
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000C2DC4 File Offset: 0x000C0FC4
		static LocalizedDescriptionAttribute()
		{
			LocalizedDescriptionAttribute.AddType(typeof(IncidentReportOriginalMail), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ADAttribute), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AddedRecipientType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DisclaimerFallbackAction), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(EvaluatedUser), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(Evaluation), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(FromUserScope), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ManagementRelationship), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(MessageType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(NotifySenderType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ToUserScope), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(RuleMode), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AuthenticationMethod), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AcceptedDomainType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ElcFolderType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(MasterType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(RetentionActionType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ServerRole), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(CopyStatus), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ContentIndexStatusType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ServerEditionType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AsyncOperationType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(ClientExtensionProvidedTo), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DefaultStateForUser), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(MailboxFolderPermissionRole), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(_DeliveryStatus), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(DeviceAccessState), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(DeviceAccessStateReason), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(DevicePolicyApplicationStatus), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(DeviceRemoteWipeStatus), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(ExtensionInstallScope), LocalizedDescriptionAttribute.enumStringsResourceManagerForOwaOption, true);
			LocalizedDescriptionAttribute.AddType(typeof(ArchiveState), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(GroupTypeFlags), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(RecipientTypeDetails), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(WellKnownRecipientType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DeviceAccessState), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(GroupNamingPolicyAttributeEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AudioCodecEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(AutoAttendantDisambiguationFieldEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DialByNamePrimaryEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DialByNameSecondaryEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DisambiguationFieldEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(DRMProtectionOptions), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(GatewayStatus), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(StatusEnum), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(UMUriType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(UMVoIPSecurityType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(SpamFilteringAction), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(SpamFilteringOption), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
			LocalizedDescriptionAttribute.AddType(typeof(TenantConnectorType), LocalizedDescriptionAttribute.enumStringsResourceManager, false);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000C3245 File Offset: 0x000C1445
		public LocalizedDescriptionAttribute()
		{
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000C324D File Offset: 0x000C144D
		public LocalizedDescriptionAttribute(LocalizedString description) : base(description)
		{
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000C3256 File Offset: 0x000C1456
		public LocalizedDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000C3264 File Offset: 0x000C1464
		public static void AddType(Type type, ExchangeResourceManager resourceManager, bool isForOwaOption = false)
		{
			if (isForOwaOption)
			{
				LocalizedDescriptionAttribute.enumType2ResourceManagerTableForOwaOption.Add(type, resourceManager);
				return;
			}
			LocalizedDescriptionAttribute.enumType2ResourceManagerTable.Add(type, resourceManager);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000C3282 File Offset: 0x000C1482
		public new static string FromEnum(Type enumType, object value)
		{
			return LocalizedDescriptionAttribute.FromEnum(enumType, value, LocalizedDescriptionAttribute.enumType2ResourceManagerTable);
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000C3290 File Offset: 0x000C1490
		public static string FromEnumForOwaOption(Type enumType, object value)
		{
			return LocalizedDescriptionAttribute.FromEnum(enumType, value, LocalizedDescriptionAttribute.enumType2ResourceManagerTableForOwaOption);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000C32A0 File Offset: 0x000C14A0
		private static string FromEnum(Type enumType, object value, Dictionary<Type, ExchangeResourceManager> enumType2ResourceManagerTable)
		{
			string text = null;
			if (enumType2ResourceManagerTable.ContainsKey(enumType))
			{
				text = LocalizedDescriptionAttribute.FromEnum(enumType, value, enumType2ResourceManagerTable[enumType]);
			}
			if (text == null)
			{
				text = LocalizedDescriptionAttribute.FromEnum(enumType, value);
			}
			return text;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000C32D4 File Offset: 0x000C14D4
		private static string FromEnum(Type enumType, object value, ExchangeResourceManager resourceManager)
		{
			string result = null;
			if (resourceManager != null)
			{
				string text = LocalizedDescriptionAttribute.Enum2LocStringId(enumType, value);
				string[] array = text.ToString().Split(new char[]
				{
					','
				});
				StringBuilder stringBuilder = new StringBuilder();
				int i;
				for (i = 0; i < array.Length; i++)
				{
					string @string = resourceManager.GetString(array[i].Trim());
					if (@string == null)
					{
						throw new InvalidOperationException(string.Format("failed to find locstring id={0}. This string is used as LocDescription of {1} value in enum type {2}", array[i], value, enumType.FullName));
					}
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(@string);
				}
				if (i == array.Length)
				{
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000C33A4 File Offset: 0x000C15A4
		private static string Enum2LocStringId(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.GetTypeInfo().IsEnum)
			{
				throw new ArgumentException("enumType must be an enum.", "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			object obj = Enum.ToObject(enumType, value);
			if (LocalizedDescriptionAttribute.enum2LocStringIdTable == null)
			{
				Dictionary<object, string> value2 = new Dictionary<object, string>();
				Interlocked.CompareExchange<Dictionary<object, string>>(ref LocalizedDescriptionAttribute.enum2LocStringIdTable, value2, null);
			}
			string text;
			if (LocalizedDescriptionAttribute.enum2LocStringIdTable.TryGetValue(obj, out text))
			{
				return text;
			}
			lock (LocalizedDescriptionAttribute.enum2LocStringIdTable)
			{
				if (LocalizedDescriptionAttribute.enum2LocStringIdTable.TryGetValue(obj, out text))
				{
					return text;
				}
				string[] array = obj.ToString().Split(new char[]
				{
					','
				});
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					string fieldName = array[i].Trim();
					string value3 = fieldName;
					foreach (FieldInfo fieldInfo in from x in enumType.GetTypeInfo().DeclaredFields
					where x.Name == fieldName
					select x)
					{
						using (IEnumerator<object> enumerator2 = fieldInfo.GetCustomAttributes(false).Where((object x) => x is LocalizedDescriptionAttribute).GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj3 = enumerator2.Current;
								value3 = ((LocalizedDescriptionAttribute)obj3).LocalizedString.Id;
							}
						}
					}
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value3);
				}
				text = stringBuilder.ToString();
				LocalizedDescriptionAttribute.enum2LocStringIdTable.Add(obj, text);
			}
			return text;
		}

		// Token: 0x04002AB3 RID: 10931
		private static Dictionary<object, string> enum2LocStringIdTable;

		// Token: 0x04002AB4 RID: 10932
		private static ExchangeResourceManager enumStringsResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.EnumStrings", typeof(EnumStrings).GetTypeInfo().Assembly);

		// Token: 0x04002AB5 RID: 10933
		private static ExchangeResourceManager enumStringsResourceManagerForOwaOption = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionEnumStrings", typeof(OwaOptionEnumStrings).GetTypeInfo().Assembly);

		// Token: 0x04002AB6 RID: 10934
		private static Dictionary<Type, ExchangeResourceManager> enumType2ResourceManagerTable = new Dictionary<Type, ExchangeResourceManager>();

		// Token: 0x04002AB7 RID: 10935
		private static Dictionary<Type, ExchangeResourceManager> enumType2ResourceManagerTableForOwaOption = new Dictionary<Type, ExchangeResourceManager>();
	}
}
