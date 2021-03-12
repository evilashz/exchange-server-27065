using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F8 RID: 2040
	internal class ObjectGuidHandler<T> : CustomPropertyHandlerBase
	{
		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x0015F9DF File Offset: 0x0015DBDF
		private bool IsString
		{
			get
			{
				return typeof(T) == typeof(string);
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x0015F9FC File Offset: 0x0015DBFC
		private Guid GetGuid(object v)
		{
			Guid result;
			if (this.IsString)
			{
				string g = (string)v;
				result = new Guid(g);
			}
			else
			{
				result = new Guid((byte[])v);
			}
			return result;
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x0015FA30 File Offset: 0x0015DC30
		private void AddValue(DirectoryAttributeModification mod, Guid v)
		{
			if (this.IsString)
			{
				mod.Add(v.ToString());
				return;
			}
			mod.Add(v.ToByteArray());
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x0015FA60 File Offset: 0x0015DC60
		public override List<ADObjectId> EnumerateObjectDependenciesInSource(TenantRelocationSyncTranslator translator, DirectoryAttribute sourceValue)
		{
			List<ADObjectId> list = new List<ADObjectId>();
			object[] values = sourceValue.GetValues(typeof(T));
			foreach (object v in values)
			{
				Guid guid = this.GetGuid(v);
				if (!guid.Equals(EmailAddressPolicy.PolicyGuid) && !Guid.Empty.Equals(guid))
				{
					list.Add(new ADObjectId(guid));
				}
			}
			return list;
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x0015FAD4 File Offset: 0x0015DCD4
		public override void UpdateModifyRequestForTarget(TenantRelocationSyncTranslator translator, DirectoryAttribute sourceValue, ref DirectoryAttributeModification mod)
		{
			object[] values = sourceValue.GetValues(typeof(T));
			foreach (object obj in values)
			{
				Guid guid = this.GetGuid(obj);
				if (guid.Equals(EmailAddressPolicy.PolicyGuid) || Guid.Empty.Equals(guid))
				{
					if (this.IsString)
					{
						mod.Add((string)obj);
					}
					else
					{
						mod.Add((byte[])obj);
					}
				}
				else
				{
					DistinguishedNameMapItem distinguishedNameMapItem = translator.Mappings.LookupByCorrelationGuid(guid);
					if (distinguishedNameMapItem == null)
					{
						this.AddValue(mod, guid);
					}
					else
					{
						this.AddValue(mod, distinguishedNameMapItem.TargetDN.ObjectGuid);
					}
				}
			}
			mod.Name = sourceValue.Name;
			mod.Operation = DirectoryAttributeOperation.Replace;
		}
	}
}
