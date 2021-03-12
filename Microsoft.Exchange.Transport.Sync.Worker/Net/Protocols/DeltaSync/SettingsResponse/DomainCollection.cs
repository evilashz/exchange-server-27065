using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200013C RID: 316
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class DomainCollection : ArrayList
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x0001C234 File Offset: 0x0001A434
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001C23F File Offset: 0x0001A43F
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001C249 File Offset: 0x0001A449
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000329 RID: 809
		public string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
