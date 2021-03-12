using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class ConfigObjectIdParameter : IIdentityParameter
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000CE40 File Offset: 0x0000B040
		public ConfigObjectIdParameter()
		{
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000CE48 File Offset: 0x0000B048
		public ConfigObjectIdParameter(string identity)
		{
			this.identity = identity;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000CE57 File Offset: 0x0000B057
		internal ConfigObjectIdParameter(ConfigObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000CE66 File Offset: 0x0000B066
		public string RawIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000CE6E File Offset: 0x0000B06E
		public static ConfigObjectIdParameter Parse(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			return new ConfigObjectIdParameter(identity);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CE89 File Offset: 0x0000B089
		public static implicit operator string(ConfigObjectIdParameter objectId)
		{
			if (objectId != null)
			{
				return objectId.identity;
			}
			return null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CE96 File Offset: 0x0000B096
		public void Initialize(ObjectId objectId)
		{
			if (objectId != null)
			{
				this.identity = objectId.ToString();
				return;
			}
			this.identity = null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		public virtual IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			ConfigObject[] array = null;
			DataSourceManager dataSourceManager = (DataSourceManager)session;
			notFoundReason = null;
			string text = null;
			if (this.identity.StartsWith("CN=", StringComparison.OrdinalIgnoreCase) || this.identity.StartsWith("OU=", StringComparison.OrdinalIgnoreCase))
			{
				text = string.Format("Identity='{0}'", this.identity);
			}
			else
			{
				try
				{
					new Guid(this.identity);
					text = string.Format("Identity='<GUID={0}>'", this.identity);
				}
				catch (FormatException)
				{
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				array = dataSourceManager.Find(typeof(T), text, true, null);
			}
			if (array == null && rootId != null)
			{
				text = string.Format("Identity='CN={0},{1}'", this.identity, rootId.ToString());
				array = dataSourceManager.Find(typeof(T), text, true, null);
			}
			if (array == null)
			{
				array = new ConfigObject[0];
			}
			return (IEnumerable<T>)((IEnumerable<IConfigurable>)array);
		}

		// Token: 0x040000DA RID: 218
		private string identity;
	}
}
