using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000196 RID: 406
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LegacyUmUserException : LocalizedException
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x00034BC1 File Offset: 0x00032DC1
		public LegacyUmUserException(string legacyDN) : base(Strings.LegacyUmUser(legacyDN))
		{
			this.legacyDN = legacyDN;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00034BD6 File Offset: 0x00032DD6
		public LegacyUmUserException(string legacyDN, Exception innerException) : base(Strings.LegacyUmUser(legacyDN), innerException)
		{
			this.legacyDN = legacyDN;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00034BEC File Offset: 0x00032DEC
		protected LegacyUmUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.legacyDN = (string)info.GetValue("legacyDN", typeof(string));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00034C16 File Offset: 0x00032E16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("legacyDN", this.legacyDN);
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00034C31 File Offset: 0x00032E31
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
		}

		// Token: 0x0400077D RID: 1917
		private readonly string legacyDN;
	}
}
