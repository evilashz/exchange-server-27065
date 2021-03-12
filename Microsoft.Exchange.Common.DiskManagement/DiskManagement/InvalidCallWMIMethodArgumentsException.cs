using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCallWMIMethodArgumentsException : BitlockerUtilException
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00004857 File Offset: 0x00002A57
		public InvalidCallWMIMethodArgumentsException(string[] inParamNameList, object inParamValueList, int inParamNameListLenght, int inParamValueListLenght) : base(DiskManagementStrings.InvalidCallWMIMethodArgumentsError(inParamNameList, inParamValueList, inParamNameListLenght, inParamValueListLenght))
		{
			this.inParamNameList = inParamNameList;
			this.inParamValueList = inParamValueList;
			this.inParamNameListLenght = inParamNameListLenght;
			this.inParamValueListLenght = inParamValueListLenght;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000488B File Offset: 0x00002A8B
		public InvalidCallWMIMethodArgumentsException(string[] inParamNameList, object inParamValueList, int inParamNameListLenght, int inParamValueListLenght, Exception innerException) : base(DiskManagementStrings.InvalidCallWMIMethodArgumentsError(inParamNameList, inParamValueList, inParamNameListLenght, inParamValueListLenght), innerException)
		{
			this.inParamNameList = inParamNameList;
			this.inParamValueList = inParamValueList;
			this.inParamNameListLenght = inParamNameListLenght;
			this.inParamValueListLenght = inParamValueListLenght;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000048C4 File Offset: 0x00002AC4
		protected InvalidCallWMIMethodArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.inParamNameList = (string[])info.GetValue("inParamNameList", typeof(string[]));
			this.inParamValueList = info.GetValue("inParamValueList", typeof(object));
			this.inParamNameListLenght = (int)info.GetValue("inParamNameListLenght", typeof(int));
			this.inParamValueListLenght = (int)info.GetValue("inParamValueListLenght", typeof(int));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004954 File Offset: 0x00002B54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("inParamNameList", this.inParamNameList);
			info.AddValue("inParamValueList", this.inParamValueList);
			info.AddValue("inParamNameListLenght", this.inParamNameListLenght);
			info.AddValue("inParamValueListLenght", this.inParamValueListLenght);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000049AD File Offset: 0x00002BAD
		public string[] InParamNameList
		{
			get
			{
				return this.inParamNameList;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000049B5 File Offset: 0x00002BB5
		public object InParamValueList
		{
			get
			{
				return this.inParamValueList;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000049BD File Offset: 0x00002BBD
		public int InParamNameListLenght
		{
			get
			{
				return this.inParamNameListLenght;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000049C5 File Offset: 0x00002BC5
		public int InParamValueListLenght
		{
			get
			{
				return this.inParamValueListLenght;
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly string[] inParamNameList;

		// Token: 0x04000054 RID: 84
		private readonly object inParamValueList;

		// Token: 0x04000055 RID: 85
		private readonly int inParamNameListLenght;

		// Token: 0x04000056 RID: 86
		private readonly int inParamValueListLenght;
	}
}
