using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000005 RID: 5
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IrresolvableConflictException : StoragePermanentException
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023CD File Offset: 0x000005CD
		public IrresolvableConflictException(ConflictResolutionResult conflictResolutionResult) : base(Strings.IrresolvableConflict(conflictResolutionResult))
		{
			this.conflictResolutionResult = conflictResolutionResult;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E2 File Offset: 0x000005E2
		public IrresolvableConflictException(ConflictResolutionResult conflictResolutionResult, Exception innerException) : base(Strings.IrresolvableConflict(conflictResolutionResult), innerException)
		{
			this.conflictResolutionResult = conflictResolutionResult;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F8 File Offset: 0x000005F8
		protected IrresolvableConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.conflictResolutionResult = (ConflictResolutionResult)info.GetValue("conflictResolutionResult", typeof(ConflictResolutionResult));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002422 File Offset: 0x00000622
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("conflictResolutionResult", this.conflictResolutionResult);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000243D File Offset: 0x0000063D
		public ConflictResolutionResult ConflictResolutionResult
		{
			get
			{
				return this.conflictResolutionResult;
			}
		}

		// Token: 0x04000015 RID: 21
		private readonly ConflictResolutionResult conflictResolutionResult;
	}
}
