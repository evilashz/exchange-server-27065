using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D0 RID: 4304
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsVersionMismatchException : LocalizedException
	{
		// Token: 0x0600B303 RID: 45827 RVA: 0x0029AA18 File Offset: 0x00298C18
		public RmsVersionMismatchException(Uri uri) : base(Strings.RmsVersionMismatchException(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600B304 RID: 45828 RVA: 0x0029AA2D File Offset: 0x00298C2D
		public RmsVersionMismatchException(Uri uri, Exception innerException) : base(Strings.RmsVersionMismatchException(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x0029AA43 File Offset: 0x00298C43
		protected RmsVersionMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (Uri)info.GetValue("uri", typeof(Uri));
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x0029AA6D File Offset: 0x00298C6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x170038E0 RID: 14560
		// (get) Token: 0x0600B307 RID: 45831 RVA: 0x0029AA88 File Offset: 0x00298C88
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04006246 RID: 25158
		private readonly Uri uri;
	}
}
