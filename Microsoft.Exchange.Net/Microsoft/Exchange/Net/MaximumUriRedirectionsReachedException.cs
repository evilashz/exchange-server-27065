using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000C4 RID: 196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MaximumUriRedirectionsReachedException : LocalizedException
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x00012544 File Offset: 0x00010744
		public MaximumUriRedirectionsReachedException(int maximumUriRedirections) : base(AuthenticationStrings.MaximumUriRedirectionsReachedException(maximumUriRedirections))
		{
			this.maximumUriRedirections = maximumUriRedirections;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012559 File Offset: 0x00010759
		public MaximumUriRedirectionsReachedException(int maximumUriRedirections, Exception innerException) : base(AuthenticationStrings.MaximumUriRedirectionsReachedException(maximumUriRedirections), innerException)
		{
			this.maximumUriRedirections = maximumUriRedirections;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001256F File Offset: 0x0001076F
		protected MaximumUriRedirectionsReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maximumUriRedirections = (int)info.GetValue("maximumUriRedirections", typeof(int));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012599 File Offset: 0x00010799
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maximumUriRedirections", this.maximumUriRedirections);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000125B4 File Offset: 0x000107B4
		public int MaximumUriRedirections
		{
			get
			{
				return this.maximumUriRedirections;
			}
		}

		// Token: 0x040003EA RID: 1002
		private readonly int maximumUriRedirections;
	}
}
