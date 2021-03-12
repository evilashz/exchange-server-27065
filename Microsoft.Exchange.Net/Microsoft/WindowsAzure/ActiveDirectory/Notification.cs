using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A3 RID: 1443
	[DataServiceKey("objectId")]
	public class Notification : DirectoryObject
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
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

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0002C9F8 File Offset: 0x0002ABF8
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

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0002CA01 File Offset: 0x0002AC01
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0002CA09 File Offset: 0x0002AC09
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

		// Token: 0x0400195C RID: 6492
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _callbackUri;

		// Token: 0x0400195D RID: 6493
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _filters = new Collection<string>();
	}
}
