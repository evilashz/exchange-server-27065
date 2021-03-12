using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BA RID: 4538
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidLanguageIdException : LocalizedException
	{
		// Token: 0x0600B88B RID: 47243 RVA: 0x002A4BA3 File Offset: 0x002A2DA3
		public InvalidLanguageIdException(string l) : base(Strings.InvalidLanguageIdException(l))
		{
			this.l = l;
		}

		// Token: 0x0600B88C RID: 47244 RVA: 0x002A4BB8 File Offset: 0x002A2DB8
		public InvalidLanguageIdException(string l, Exception innerException) : base(Strings.InvalidLanguageIdException(l), innerException)
		{
			this.l = l;
		}

		// Token: 0x0600B88D RID: 47245 RVA: 0x002A4BCE File Offset: 0x002A2DCE
		protected InvalidLanguageIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.l = (string)info.GetValue("l", typeof(string));
		}

		// Token: 0x0600B88E RID: 47246 RVA: 0x002A4BF8 File Offset: 0x002A2DF8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("l", this.l);
		}

		// Token: 0x17003A20 RID: 14880
		// (get) Token: 0x0600B88F RID: 47247 RVA: 0x002A4C13 File Offset: 0x002A2E13
		public string L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x0400643B RID: 25659
		private readonly string l;
	}
}
