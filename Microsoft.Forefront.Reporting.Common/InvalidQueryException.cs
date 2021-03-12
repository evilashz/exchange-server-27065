using System;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x0200000A RID: 10
	public class InvalidQueryException : ReportingException
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000020FC File Offset: 0x000002FC
		internal InvalidQueryException()
		{
			this.ErrorCode = InvalidQueryException.InvalidQueryErrorCode.InvalidQueryFormat;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000210B File Offset: 0x0000030B
		internal InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode errorCode) : base(errorCode.ToString())
		{
			this.ErrorCode = errorCode;
			this.Position = 0;
			this.ErrorProperty = string.Empty;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002137 File Offset: 0x00000337
		internal InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode errorCode, string propertyName, int position) : base(string.Format("Error:{0} PropertyName:{1} Position:{2}", errorCode, propertyName, position))
		{
			this.ErrorCode = errorCode;
			this.ErrorProperty = propertyName;
			this.Position = position;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000216B File Offset: 0x0000036B
		internal InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode errorCode, string propertyName, int position, Exception innerException) : base(string.Format("Error:{0} PropertyName:{1} Position:{2}", errorCode, propertyName, position), innerException)
		{
			this.ErrorCode = errorCode;
			this.ErrorProperty = propertyName;
			this.Position = position;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021A1 File Offset: 0x000003A1
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021A9 File Offset: 0x000003A9
		public InvalidQueryException.InvalidQueryErrorCode ErrorCode { get; internal set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021B2 File Offset: 0x000003B2
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021BA File Offset: 0x000003BA
		public string ErrorProperty { get; internal set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021C3 File Offset: 0x000003C3
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000021CB File Offset: 0x000003CB
		public int Position { get; internal set; }

		// Token: 0x0200000B RID: 11
		public enum InvalidQueryErrorCode
		{
			// Token: 0x0400002D RID: 45
			InvalidQueryFormat,
			// Token: 0x0400002E RID: 46
			EmptySearchDefinition,
			// Token: 0x0400002F RID: 47
			MissingRequiredProperty,
			// Token: 0x04000030 RID: 48
			MissingQuote,
			// Token: 0x04000031 RID: 49
			InvalidValue,
			// Token: 0x04000032 RID: 50
			UnsupportedValue,
			// Token: 0x04000033 RID: 51
			ValueOutRange,
			// Token: 0x04000034 RID: 52
			InvalidProperty,
			// Token: 0x04000035 RID: 53
			UnsupportedProperty,
			// Token: 0x04000036 RID: 54
			InvalidGrouper,
			// Token: 0x04000037 RID: 55
			UnpairedParenthese,
			// Token: 0x04000038 RID: 56
			DoesNotMeetMinimalQueryRequirement,
			// Token: 0x04000039 RID: 57
			DuplicateField,
			// Token: 0x0400003A RID: 58
			InvalidQueryType,
			// Token: 0x0400003B RID: 59
			ToManyPropertiesInGroup,
			// Token: 0x0400003C RID: 60
			ToManyGroups
		}
	}
}
