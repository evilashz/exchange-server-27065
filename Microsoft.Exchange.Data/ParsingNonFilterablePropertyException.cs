using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DB RID: 219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingNonFilterablePropertyException : ParsingException
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x0001A72C File Offset: 0x0001892C
		public ParsingNonFilterablePropertyException(string propertyName, string invalidQuery, int position) : base(DataStrings.ExceptionParseNonFilterablePropertyError(propertyName, invalidQuery, position))
		{
			this.propertyName = propertyName;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001A751 File Offset: 0x00018951
		public ParsingNonFilterablePropertyException(string propertyName, string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionParseNonFilterablePropertyError(propertyName, invalidQuery, position), innerException)
		{
			this.propertyName = propertyName;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001A778 File Offset: 0x00018978
		protected ParsingNonFilterablePropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001A7ED File Offset: 0x000189ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001A82A File Offset: 0x00018A2A
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001A832 File Offset: 0x00018A32
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001A83A File Offset: 0x00018A3A
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000572 RID: 1394
		private readonly string propertyName;

		// Token: 0x04000573 RID: 1395
		private readonly string invalidQuery;

		// Token: 0x04000574 RID: 1396
		private readonly int position;
	}
}
