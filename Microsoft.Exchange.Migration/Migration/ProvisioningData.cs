using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003D RID: 61
	internal abstract class ProvisioningData : IProvisioningData
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000AA36 File Offset: 0x00008C36
		public Dictionary<string, object> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000AA3E File Offset: 0x00008C3E
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public ProvisioningType ProvisioningType
		{
			get
			{
				return this.GetEnumValueOrDefault<ProvisioningType>("ProvisioningType", ProvisioningType.Unknown);
			}
			set
			{
				this["ProvisioningType"] = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000AA5F File Offset: 0x00008C5F
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000AA6D File Offset: 0x00008C6D
		public ProvisioningAction Action
		{
			get
			{
				return this.GetEnumValueOrDefault<ProvisioningAction>("ProvisioningAction", ProvisioningAction.CreateNew);
			}
			set
			{
				this["ProvisioningAction"] = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000AA80 File Offset: 0x00008C80
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public int Version
		{
			get
			{
				object obj = this["Version"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				this["Version"] = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000AAB7 File Offset: 0x00008CB7
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000AAC5 File Offset: 0x00008CC5
		public ProvisioningComponent Component
		{
			get
			{
				return this.GetEnumValueOrDefault<ProvisioningComponent>("ProvisioningComponent", ProvisioningComponent.BulkProvision);
			}
			set
			{
				this["ProvisioningComponent"] = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000AAEA File Offset: 0x00008CEA
		public string Identity
		{
			get
			{
				return (string)this["Identity"];
			}
			set
			{
				this["Identity"] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public string Organization
		{
			get
			{
				return (string)this["Organization"];
			}
			set
			{
				this["Organization"] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000AB18 File Offset: 0x00008D18
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public bool IsBPOS
		{
			get
			{
				object obj = this["IsBPOS"];
				return obj != null && (bool)obj;
			}
			set
			{
				this["IsBPOS"] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		protected object this[string key]
		{
			get
			{
				object result;
				if (!this.Parameters.TryGetValue(key, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.Parameters[key] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		protected object this[PropertyDefinition cmdletParameterDefinition]
		{
			get
			{
				return this[cmdletParameterDefinition.Name];
			}
			set
			{
				this[cmdletParameterDefinition.Name] = value;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public static IProvisioningData FromPersistableDictionary(PersistableDictionary dictionary)
		{
			IProvisioningData provisioningData = null;
			string text = (string)((IDictionary)dictionary)["ProvisioningType"];
			try
			{
				ProvisioningType type = (ProvisioningType)Enum.Parse(typeof(ProvisioningType), text);
				provisioningData = ProvisioningDataFactory.Create(type);
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					provisioningData.Parameters[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			catch (ArgumentException)
			{
				throw new MigrationDataCorruptionException("Unable to create from PersistableDictionary ProvisioningData of type " + text);
			}
			return provisioningData;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AC60 File Offset: 0x00008E60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				stringBuilder.Append(string.Format("{0}:", keyValuePair.Key));
				if (string.Compare(keyValuePair.Key, "Password", StringComparison.OrdinalIgnoreCase) != 0)
				{
					stringBuilder.AppendLine(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public PersistableDictionary ToPersistableDictionary()
		{
			PersistableDictionary persistableDictionary = new PersistableDictionary();
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				object obj = keyValuePair.Value;
				if (obj.GetType().IsEnum)
				{
					obj = obj.ToString();
				}
				persistableDictionary.Add(keyValuePair.Key, obj);
			}
			return persistableDictionary;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AD78 File Offset: 0x00008F78
		private T GetEnumValueOrDefault<T>(string propertyName, T defaultValue)
		{
			object obj = this[propertyName];
			if (obj == null || !typeof(T).IsEnum)
			{
				return defaultValue;
			}
			if (obj.GetType() == typeof(T))
			{
				return (T)((object)obj);
			}
			T result = defaultValue;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), (string)obj));
			}
			catch (ArgumentException exception)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception, string.Format("Unable to parse enum value {0} for parameter {1}, using default value {2} instead.", obj, propertyName, defaultValue.ToString()), new object[0]);
			}
			return result;
		}

		// Token: 0x040000DA RID: 218
		public const string IdentityParameterName = "Identity";

		// Token: 0x040000DB RID: 219
		public const string OrganizationParameterName = "Organization";

		// Token: 0x040000DC RID: 220
		public const string ProvisioningTypeParameterName = "ProvisioningType";

		// Token: 0x040000DD RID: 221
		public const string ProvisioningActionParameterName = "ProvisioningAction";

		// Token: 0x040000DE RID: 222
		public const string ProvisioningComponentParameterName = "ProvisioningComponent";

		// Token: 0x040000DF RID: 223
		public const string PasswordParameterName = "Password";

		// Token: 0x040000E0 RID: 224
		public const string VersionParameterName = "Version";

		// Token: 0x040000E1 RID: 225
		public const int CurrentVersion = 1;

		// Token: 0x040000E2 RID: 226
		public const string IsBPOSParameterName = "IsBPOS";

		// Token: 0x040000E3 RID: 227
		private Dictionary<string, object> parameters = new Dictionary<string, object>();
	}
}
