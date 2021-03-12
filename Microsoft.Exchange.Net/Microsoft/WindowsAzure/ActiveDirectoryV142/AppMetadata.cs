using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005D7 RID: 1495
	public class AppMetadata
	{
		// Token: 0x06001858 RID: 6232 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static AppMetadata CreateAppMetadata(int version, Collection<AppMetadataEntry> data)
		{
			AppMetadata appMetadata = new AppMetadata();
			appMetadata.version = version;
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			appMetadata.data = data;
			return appMetadata;
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0002F7D4 File Offset: 0x0002D9D4
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0002F7DC File Offset: 0x0002D9DC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0002F7E5 File Offset: 0x0002D9E5
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0002F7ED File Offset: 0x0002D9ED
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AppMetadataEntry> data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x04001B0C RID: 6924
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int _version;

		// Token: 0x04001B0D RID: 6925
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppMetadataEntry> _data = new Collection<AppMetadataEntry>();
	}
}
