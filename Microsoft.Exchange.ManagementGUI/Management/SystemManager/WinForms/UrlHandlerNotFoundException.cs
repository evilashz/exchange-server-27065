using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UrlHandlerNotFoundException : LocalizedException
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x00036F90 File Offset: 0x00035190
		public UrlHandlerNotFoundException(string url) : base(Strings.FailToOpenURL(url))
		{
			this.url = url;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00036FA5 File Offset: 0x000351A5
		public UrlHandlerNotFoundException(string url, Exception innerException) : base(Strings.FailToOpenURL(url), innerException)
		{
			this.url = url;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00036FBB File Offset: 0x000351BB
		protected UrlHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.url = (string)info.GetValue("url", typeof(string));
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00036FE5 File Offset: 0x000351E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("url", this.url);
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00037000 File Offset: 0x00035200
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04001070 RID: 4208
		private readonly string url;
	}
}
