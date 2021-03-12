using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005EC RID: 1516
	[DataServiceKey("objectId")]
	public class Notification : DirectoryObject
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x00030AD4 File Offset: 0x0002ECD4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Notification CreateNotification(string objectId, Collection<string> filters)
		{
			Notification notification = new Notification();
			notification.objectId = objectId;
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			notification.filters = filters;
			return notification;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00030B04 File Offset: 0x0002ED04
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00030B0C File Offset: 0x0002ED0C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string callbackUri
		{
			get
			{
				return this._callbackUri;
			}
			set
			{
				this._callbackUri = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00030B15 File Offset: 0x0002ED15
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x00030B1D File Offset: 0x0002ED1D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> filters
		{
			get
			{
				return this._filters;
			}
			set
			{
				this._filters = value;
			}
		}

		// Token: 0x04001BC1 RID: 7105
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _callbackUri;

		// Token: 0x04001BC2 RID: 7106
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _filters = new Collection<string>();
	}
}
