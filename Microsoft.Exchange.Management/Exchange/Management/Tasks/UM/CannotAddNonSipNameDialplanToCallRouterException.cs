using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001205 RID: 4613
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotAddNonSipNameDialplanToCallRouterException : LocalizedException
	{
		// Token: 0x0600B9EF RID: 47599 RVA: 0x002A69BB File Offset: 0x002A4BBB
		public CannotAddNonSipNameDialplanToCallRouterException(string s) : base(Strings.CannotAddNonSipNameDialplanToCallRouter(s))
		{
			this.s = s;
		}

		// Token: 0x0600B9F0 RID: 47600 RVA: 0x002A69D0 File Offset: 0x002A4BD0
		public CannotAddNonSipNameDialplanToCallRouterException(string s, Exception innerException) : base(Strings.CannotAddNonSipNameDialplanToCallRouter(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B9F1 RID: 47601 RVA: 0x002A69E6 File Offset: 0x002A4BE6
		protected CannotAddNonSipNameDialplanToCallRouterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B9F2 RID: 47602 RVA: 0x002A6A10 File Offset: 0x002A4C10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A58 RID: 14936
		// (get) Token: 0x0600B9F3 RID: 47603 RVA: 0x002A6A2B File Offset: 0x002A4C2B
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006473 RID: 25715
		private readonly string s;
	}
}
