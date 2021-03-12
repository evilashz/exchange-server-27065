using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DC RID: 220
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingNonFilterablePropertyWithListException : ParsingException
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x0001A842 File Offset: 0x00018A42
		public ParsingNonFilterablePropertyWithListException(string propertyName, string knownProperties, string invalidQuery, int position) : base(DataStrings.ExceptionParseNonFilterablePropertyErrorWithList(propertyName, knownProperties, invalidQuery, position))
		{
			this.propertyName = propertyName;
			this.knownProperties = knownProperties;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001A871 File Offset: 0x00018A71
		public ParsingNonFilterablePropertyWithListException(string propertyName, string knownProperties, string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionParseNonFilterablePropertyErrorWithList(propertyName, knownProperties, invalidQuery, position), innerException)
		{
			this.propertyName = propertyName;
			this.knownProperties = knownProperties;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		protected ParsingNonFilterablePropertyWithListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.knownProperties = (string)info.GetValue("knownProperties", typeof(string));
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001A93C File Offset: 0x00018B3C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("knownProperties", this.knownProperties);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001A995 File Offset: 0x00018B95
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001A99D File Offset: 0x00018B9D
		public string KnownProperties
		{
			get
			{
				return this.knownProperties;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001A9A5 File Offset: 0x00018BA5
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001A9AD File Offset: 0x00018BAD
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000575 RID: 1397
		private readonly string propertyName;

		// Token: 0x04000576 RID: 1398
		private readonly string knownProperties;

		// Token: 0x04000577 RID: 1399
		private readonly string invalidQuery;

		// Token: 0x04000578 RID: 1400
		private readonly int position;
	}
}
