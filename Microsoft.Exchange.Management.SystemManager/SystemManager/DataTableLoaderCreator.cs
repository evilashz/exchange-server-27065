using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000013 RID: 19
	public class DataTableLoaderCreator : IDataTableLoaderCreator
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005510 File Offset: 0x00003710
		public DataTableLoaderCreator(ObjectPickerProfileLoader profileLoader) : this(profileLoader, null)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000551A File Offset: 0x0000371A
		public DataTableLoaderCreator(ObjectPickerProfileLoader profileLoader, string profileName)
		{
			if (profileLoader == null)
			{
				throw new ArgumentNullException("profileLoader");
			}
			this.ProfileLoader = profileLoader;
			this.ProfileName = profileName;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005549 File Offset: 0x00003749
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005551 File Offset: 0x00003751
		public ObjectPickerProfileLoader ProfileLoader { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000555A File Offset: 0x0000375A
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005562 File Offset: 0x00003762
		public string ProfileName { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000556B File Offset: 0x0000376B
		public Dictionary<string, object> InputValues
		{
			get
			{
				return this.inputValues;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005574 File Offset: 0x00003774
		public DataTableLoader CreateDataTableLoader(string name)
		{
			DataTableLoader dataTableLoader = new DataTableLoader(this.ProfileLoader, string.IsNullOrEmpty(this.ProfileName) ? name : this.ProfileName);
			foreach (string text in this.InputValues.Keys)
			{
				dataTableLoader.InputValue(text, this.InputValues[text]);
			}
			return dataTableLoader;
		}

		// Token: 0x0400003F RID: 63
		private Dictionary<string, object> inputValues = new Dictionary<string, object>();
	}
}
