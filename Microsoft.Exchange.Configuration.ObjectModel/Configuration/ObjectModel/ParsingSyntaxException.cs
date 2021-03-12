using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A9 RID: 681
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingSyntaxException : ParsingException
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x0005C342 File Offset: 0x0005A542
		public ParsingSyntaxException(string invalidQuery, int position) : base(Strings.ExceptionParseError(invalidQuery, position))
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005C35F File Offset: 0x0005A55F
		public ParsingSyntaxException(string invalidQuery, int position, Exception innerException) : base(Strings.ExceptionParseError(invalidQuery, position), innerException)
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0005C380 File Offset: 0x0005A580
		protected ParsingSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005C3D5 File Offset: 0x0005A5D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0005C401 File Offset: 0x0005A601
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005C409 File Offset: 0x0005A609
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000980 RID: 2432
		private readonly string invalidQuery;

		// Token: 0x04000981 RID: 2433
		private readonly int position;
	}
}
