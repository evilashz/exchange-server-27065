using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B8 RID: 184
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyPropertiesResultFactory : ResultFactory, IProgressResultFactory
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000E901 File Offset: 0x0000CB01
		internal CopyPropertiesResultFactory(byte logonId, uint destinationObjectHandleIndex)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
			this.logonId = logonId;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000E918 File Offset: 0x0000CB18
		internal CopyPropertiesResultFactory(object progressToken)
		{
			if (progressToken == null)
			{
				throw new ArgumentNullException("progressToken");
			}
			LogonDestinationHandleProgressToken logonDestinationHandleProgressToken = (LogonDestinationHandleProgressToken)progressToken;
			if (logonDestinationHandleProgressToken.RopId != RopId.CopyProperties)
			{
				throw new ArgumentException("Incorrect progress token, token's RopId: " + logonDestinationHandleProgressToken.RopId, "progressToken");
			}
			this.destinationObjectHandleIndex = logonDestinationHandleProgressToken.DestinationObjectHandleIndex;
			this.logonId = logonDestinationHandleProgressToken.LogonId;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000E994 File Offset: 0x0000CB94
		public static RopResult Parse(Reader reader)
		{
			return ResultFactory.ParseResultOrProgress(RopId.CopyProperties, reader, (Reader resultReader) => new SuccessfulCopyPropertiesResult(resultReader), (Reader resultReader) => new FailedCopyPropertiesResult(resultReader));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new FailedCopyPropertiesResult(errorCode, this.destinationObjectHandleIndex);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000E9FA File Offset: 0x0000CBFA
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulCopyPropertiesResult(propertyProblems);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000EA02 File Offset: 0x0000CC02
		public object CreateProgressToken()
		{
			return new LogonDestinationHandleProgressToken(RopId.CopyProperties, this.destinationObjectHandleIndex, this.logonId);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000EA17 File Offset: 0x0000CC17
		public RopResult CreateProgressResult(uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(this.logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x040002D1 RID: 721
		private readonly byte logonId;

		// Token: 0x040002D2 RID: 722
		private readonly uint destinationObjectHandleIndex;
	}
}
