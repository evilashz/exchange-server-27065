using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Entities.DataModel.Serialization
{
	// Token: 0x02000004 RID: 4
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValueOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000219D File Offset: 0x0000039D
		public ValueOutOfRangeException(string name, object value) : base(Strings.ValueIsOutOfRange(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BF File Offset: 0x000003BF
		public ValueOutOfRangeException(string name, object value, Exception innerException) : base(Strings.ValueIsOutOfRange(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E4 File Offset: 0x000003E4
		protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = info.GetValue("value", typeof(object));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002234 File Offset: 0x00000434
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002268 File Offset: 0x00000468
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000007 RID: 7
		private readonly string name;

		// Token: 0x04000008 RID: 8
		private readonly object value;
	}
}
