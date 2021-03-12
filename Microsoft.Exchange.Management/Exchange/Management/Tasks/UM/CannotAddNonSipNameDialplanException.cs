using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001204 RID: 4612
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotAddNonSipNameDialplanException : LocalizedException
	{
		// Token: 0x0600B9EA RID: 47594 RVA: 0x002A6943 File Offset: 0x002A4B43
		public CannotAddNonSipNameDialplanException(string s) : base(Strings.CannotAddNonSipNameDialplan(s))
		{
			this.s = s;
		}

		// Token: 0x0600B9EB RID: 47595 RVA: 0x002A6958 File Offset: 0x002A4B58
		public CannotAddNonSipNameDialplanException(string s, Exception innerException) : base(Strings.CannotAddNonSipNameDialplan(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B9EC RID: 47596 RVA: 0x002A696E File Offset: 0x002A4B6E
		protected CannotAddNonSipNameDialplanException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B9ED RID: 47597 RVA: 0x002A6998 File Offset: 0x002A4B98
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A57 RID: 14935
		// (get) Token: 0x0600B9EE RID: 47598 RVA: 0x002A69B3 File Offset: 0x002A4BB3
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006472 RID: 25714
		private readonly string s;
	}
}
