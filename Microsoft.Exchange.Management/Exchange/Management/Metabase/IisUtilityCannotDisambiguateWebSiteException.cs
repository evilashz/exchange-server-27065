using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAA RID: 4010
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisUtilityCannotDisambiguateWebSiteException : LocalizedException
	{
		// Token: 0x0600AD1D RID: 44317 RVA: 0x00291092 File Offset: 0x0028F292
		public IisUtilityCannotDisambiguateWebSiteException(string webSiteName, string path1, string path2) : base(Strings.IisUtilityCannotDisambiguateWebSiteException(webSiteName, path1, path2))
		{
			this.webSiteName = webSiteName;
			this.path1 = path1;
			this.path2 = path2;
		}

		// Token: 0x0600AD1E RID: 44318 RVA: 0x002910B7 File Offset: 0x0028F2B7
		public IisUtilityCannotDisambiguateWebSiteException(string webSiteName, string path1, string path2, Exception innerException) : base(Strings.IisUtilityCannotDisambiguateWebSiteException(webSiteName, path1, path2), innerException)
		{
			this.webSiteName = webSiteName;
			this.path1 = path1;
			this.path2 = path2;
		}

		// Token: 0x0600AD1F RID: 44319 RVA: 0x002910E0 File Offset: 0x0028F2E0
		protected IisUtilityCannotDisambiguateWebSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.webSiteName = (string)info.GetValue("webSiteName", typeof(string));
			this.path1 = (string)info.GetValue("path1", typeof(string));
			this.path2 = (string)info.GetValue("path2", typeof(string));
		}

		// Token: 0x0600AD20 RID: 44320 RVA: 0x00291155 File Offset: 0x0028F355
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("webSiteName", this.webSiteName);
			info.AddValue("path1", this.path1);
			info.AddValue("path2", this.path2);
		}

		// Token: 0x17003792 RID: 14226
		// (get) Token: 0x0600AD21 RID: 44321 RVA: 0x00291192 File Offset: 0x0028F392
		public string WebSiteName
		{
			get
			{
				return this.webSiteName;
			}
		}

		// Token: 0x17003793 RID: 14227
		// (get) Token: 0x0600AD22 RID: 44322 RVA: 0x0029119A File Offset: 0x0028F39A
		public string Path1
		{
			get
			{
				return this.path1;
			}
		}

		// Token: 0x17003794 RID: 14228
		// (get) Token: 0x0600AD23 RID: 44323 RVA: 0x002911A2 File Offset: 0x0028F3A2
		public string Path2
		{
			get
			{
				return this.path2;
			}
		}

		// Token: 0x040060F8 RID: 24824
		private readonly string webSiteName;

		// Token: 0x040060F9 RID: 24825
		private readonly string path1;

		// Token: 0x040060FA RID: 24826
		private readonly string path2;
	}
}
