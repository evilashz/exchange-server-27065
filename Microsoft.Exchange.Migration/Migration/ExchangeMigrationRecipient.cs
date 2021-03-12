using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200009E RID: 158
	internal abstract class ExchangeMigrationRecipient : IMigrationSerializable
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00026924 File Offset: 0x00024B24
		protected ExchangeMigrationRecipient(MigrationUserRecipientType recipientType)
		{
			this.Properties = new Dictionary<PropTag, object>(ExchangeMigrationRecipient.AllRecipientProperties.Length);
			this.RecipientType = recipientType;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00026945 File Offset: 0x00024B45
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x00026952 File Offset: 0x00024B52
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return this.GetPropertyValue<MigrationUserRecipientType>(PropTag.DisplayType);
			}
			private set
			{
				this.SetPropertyValue(PropTag.DisplayType, (int)value);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00026968 File Offset: 0x00024B68
		public virtual string Identifier
		{
			get
			{
				foreach (PropTag key in new PropTag[]
				{
					PropTag.SmtpAddress,
					PropTag.EmailAddress
				})
				{
					object obj;
					if (this.Properties.TryGetValue(key, out obj))
					{
						string text = obj as string;
						if (!string.IsNullOrEmpty(text))
						{
							return text;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060008F7 RID: 2295
		public abstract HashSet<PropTag> SupportedProperties { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060008F8 RID: 2296
		public abstract HashSet<PropTag> RequiredProperties { get; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000269D2 File Offset: 0x00024BD2
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x000269DA File Offset: 0x00024BDA
		public bool DoesADObjectExist
		{
			get
			{
				return this.doesADObjectExist;
			}
			set
			{
				this.doesADObjectExist = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000269E3 File Offset: 0x00024BE3
		PropertyDefinition[] IMigrationSerializable.PropertyDefinitions
		{
			get
			{
				return ExchangeMigrationRecipient.ExchangeMigrationRecipientPropertyDefinitions;
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000269EC File Offset: 0x00024BEC
		public bool TryGetPropertyValue<T>(PropTag proptag, out T value)
		{
			object obj;
			if (this.Properties.TryGetValue(proptag, out obj))
			{
				try
				{
					value = (T)((object)obj);
					return true;
				}
				catch (InvalidCastException)
				{
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00026A38 File Offset: 0x00024C38
		public T GetPropertyValue<T>(PropTag proptag)
		{
			return (T)((object)this.Properties[proptag]);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00026A4B File Offset: 0x00024C4B
		public void SetPropertyValue(PropTag proptag, object value)
		{
			this.Properties[proptag] = value;
			this.UpdateDependentProperties(proptag);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00026A64 File Offset: 0x00024C64
		public virtual bool TryValidateRequiredProperties(out LocalizedString errorMessage)
		{
			foreach (PropTag propTag in this.RequiredProperties)
			{
				object obj;
				if (!this.Properties.TryGetValue(propTag, out obj))
				{
					errorMessage = ServerStrings.MigrationNSPIMissingRequiredField(propTag);
					return false;
				}
			}
			errorMessage = LocalizedString.Empty;
			return true;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00026AE0 File Offset: 0x00024CE0
		public override string ToString()
		{
			return this.RecipientType + ":" + this.Identifier;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00026B00 File Offset: 0x00024D00
		public virtual void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			PersistableDictionary persistableDictionary = new PersistableDictionary();
			foreach (KeyValuePair<PropTag, object> keyValuePair in this.Properties)
			{
				persistableDictionary.Add((long)((ulong)keyValuePair.Key), keyValuePair.Value);
			}
			MigrationHelper.SetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRecipientProperties, persistableDictionary);
			message[MigrationBatchMessageSchema.MigrationJobItemADObjectExists] = this.DoesADObjectExist;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00026B9C File Offset: 0x00024D9C
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("ExchangeMigrationRecipient");
			foreach (PropTag propTag in this.Properties.Keys)
			{
				string expandedName = "PropTag" + propTag.ToString();
				object obj = this.Properties[propTag];
				object[] array = obj as object[];
				if (array != null)
				{
					XElement xelement2 = new XElement(expandedName);
					foreach (object obj2 in array)
					{
						xelement2.Add(new XElement("Element", new object[]
						{
							new XAttribute("type", obj2.GetType()),
							obj2
						}));
					}
					xelement.Add(xelement2);
				}
				else
				{
					xelement.Add(new XElement(expandedName, new object[]
					{
						new XAttribute("type", obj.GetType()),
						obj
					}));
				}
			}
			return xelement;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public virtual bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			DictionaryBase dictionaryProperty = MigrationHelper.GetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRecipientProperties, false);
			if (dictionaryProperty == null)
			{
				return false;
			}
			foreach (object obj in dictionaryProperty)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				try
				{
					long num = (long)dictionaryEntry.Key;
					PropTag propTag = (PropTag)num;
					if (this.IsPropertySupported(propTag))
					{
						this.Properties[propTag] = dictionaryEntry.Value;
					}
					else
					{
						MigrationLogger.Log(MigrationEventType.Warning, "ExchangeMigrationRecipient.ReadFromMessageItem found an unsupported PropTag '{0}' msgid: '{1}'- versioning issue?", new object[]
						{
							propTag,
							message.Id
						});
					}
				}
				catch (InvalidCastException innerException)
				{
					throw new InvalidDataException("Invalid Exchange Recipient Properties. Message ID: " + message.Id, innerException);
				}
			}
			this.DoesADObjectExist = message.GetValueOrDefault<bool>(MigrationBatchMessageSchema.MigrationJobItemADObjectExists, false);
			return true;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00026DE4 File Offset: 0x00024FE4
		internal static bool TryCreate(MigrationUserRecipientType recipientType, out ExchangeMigrationRecipient recipient, bool isPAW = true)
		{
			switch (recipientType)
			{
			case MigrationUserRecipientType.Mailbox:
				recipient = new ExchangeMigrationMailUserRecipient();
				return true;
			case MigrationUserRecipientType.Contact:
				recipient = new ExchangeMigrationMailContactRecipient();
				return true;
			case MigrationUserRecipientType.Group:
				if (isPAW)
				{
					recipient = new ExchangeMigrationGroupRecipient();
				}
				else
				{
					recipient = new LegacyExchangeMigrationGroupRecipient();
				}
				return true;
			case MigrationUserRecipientType.Mailuser:
				recipient = new ExchangeMigrationMailEnabledUserRecipient();
				return true;
			}
			recipient = null;
			return false;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00026E48 File Offset: 0x00025048
		internal static ExchangeMigrationRecipient Create(IMigrationStoreObject message, MigrationUserRecipientType recipientType, bool isPAW = true)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			ExchangeMigrationRecipient exchangeMigrationRecipient;
			if (!ExchangeMigrationRecipient.TryCreate(recipientType, out exchangeMigrationRecipient, isPAW))
			{
				return null;
			}
			if (exchangeMigrationRecipient.ReadFromMessageItem(message))
			{
				return exchangeMigrationRecipient;
			}
			MigrationUtil.AssertOrThrow(isPAW, "Pre-PAW we expect to always find a recipient because it is discovered at job-item creation time.", new object[0]);
			return null;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00026E8A File Offset: 0x0002508A
		internal bool IsPropertySupported(PropTag propTag)
		{
			return this.SupportedProperties.Contains(propTag);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00026E98 File Offset: 0x00025098
		internal bool TryGetProxyAddresses(out string[] proxyAddresses)
		{
			bool flag = this.TryGetPropertyValue<string[]>((PropTag)2148470815U, out proxyAddresses);
			if (proxyAddresses != null)
			{
				List<string> list = new List<string>(proxyAddresses.Length);
				foreach (string text in proxyAddresses)
				{
					if (!string.IsNullOrEmpty(text) && !text.StartsWith("EUM:", StringComparison.OrdinalIgnoreCase) && (!text.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase) || SmtpAddress.IsValidSmtpAddress(text.Substring(5))))
					{
						list.Add(text);
					}
				}
				if (list.Count < proxyAddresses.Length)
				{
					proxyAddresses = list.ToArray();
				}
			}
			if (!ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("MigrationSourceMailboxLegacyExchangeDNStampingEnabled"))
			{
				return flag;
			}
			string text2;
			bool flag2 = this.TryGetPropertyValue<string>(PropTag.EmailAddress, out text2);
			if (!flag && !flag2)
			{
				return false;
			}
			if (!flag2)
			{
				return flag;
			}
			text2 = "X500:" + text2;
			if (proxyAddresses != null)
			{
				foreach (string a in proxyAddresses)
				{
					if (string.Equals(a, text2, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				Array.Resize<string>(ref proxyAddresses, proxyAddresses.Length + 1);
			}
			else
			{
				proxyAddresses = new string[1];
			}
			proxyAddresses[proxyAddresses.Length - 1] = text2;
			return true;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00026FBE File Offset: 0x000251BE
		internal bool IsPropertyRequired(PropTag propTag)
		{
			return this.RequiredProperties.Contains(propTag);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00026FCC File Offset: 0x000251CC
		protected virtual void UpdateDependentProperties(PropTag proptag)
		{
		}

		// Token: 0x04000376 RID: 886
		public const PropTag PropTagHomeMDB = (PropTag)2147876895U;

		// Token: 0x04000377 RID: 887
		public const PropTag PropTagPublicDelegates = (PropTag)2148864031U;

		// Token: 0x04000378 RID: 888
		public const PropTag PropTagManager = (PropTag)2147811359U;

		// Token: 0x04000379 RID: 889
		public const PropTag PropTagRoomCapacity = (PropTag)134676483U;

		// Token: 0x0400037A RID: 890
		public const PropTag PropTagUMSpokenName = (PropTag)2361524482U;

		// Token: 0x0400037B RID: 891
		public const PropTag PropTagManagedBy = (PropTag)2148270111U;

		// Token: 0x0400037C RID: 892
		public const PropTag PropTagTargetAddress = (PropTag)2148597791U;

		// Token: 0x0400037D RID: 893
		public const PropTag PropTagCountryName = (PropTag)2154364959U;

		// Token: 0x0400037E RID: 894
		public const PropTag PropTagUserCulture = (PropTag)2359230495U;

		// Token: 0x0400037F RID: 895
		public const PropTag PropTagNickname = PropTag.Account;

		// Token: 0x04000380 RID: 896
		public static readonly long[] AllRecipientProperties = new long[]
		{
			956301315L,
			956628995L,
			975372319L,
			973602847L,
			974520351L,
			974651423L,
			805371935L,
			805503007L,
			973471775L,
			973733919L,
			974913567L,
			974716959L,
			972947487L,
			974192671L,
			974585887L,
			2148470815L,
			973668383L,
			975765535L,
			975634463L,
			975699999L,
			975831071L,
			805568543L,
			2147876895L,
			2147811359L,
			2148864031L,
			134676483L,
			2361524482L,
			2148270111L,
			2154364959L,
			2359230495L,
			973078559L
		};

		// Token: 0x04000381 RID: 897
		public static readonly PropertyDefinition[] ExchangeMigrationRecipientPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemExchangeRecipientIndex,
				MigrationBatchMessageSchema.MigrationJobItemExchangeMsExchHomeServerName,
				MigrationBatchMessageSchema.MigrationJobItemExchangeRecipientProperties,
				MigrationBatchMessageSchema.MigrationJobItemLastProvisionedMemberIndex,
				MigrationBatchMessageSchema.MigrationJobItemADObjectExists
			},
			LegacyExchangeMigrationGroupRecipient.GroupPropertyDefinitions
		});

		// Token: 0x04000382 RID: 898
		protected readonly Dictionary<PropTag, object> Properties;

		// Token: 0x04000383 RID: 899
		private bool doesADObjectExist;
	}
}
