using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000592 RID: 1426
	public class AppMetadata
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0002BA50 File Offset: 0x00029C50
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0002BA80 File Offset: 0x00029C80
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x0002BA88 File Offset: 0x00029C88
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0002BA91 File Offset: 0x00029C91
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0002BA99 File Offset: 0x00029C99
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

		// Token: 0x040018CC RID: 6348
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int _version;

		// Token: 0x040018CD RID: 6349
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppMetadataEntry> _data = new Collection<AppMetadataEntry>();
	}
}
