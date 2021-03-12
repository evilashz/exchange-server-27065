using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000073 RID: 115
	internal abstract class IcsUpload : ServerObject, IServiceProvider<IcsStateHelper>, IIcsStateCheckpoint
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0002061C File Offset: 0x0001E81C
		public IcsUpload(ReferenceCount<CoreFolder> sourceFolder, Logon logon) : base(logon)
		{
			if (logon == null)
			{
				throw new ArgumentNullException("logon");
			}
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.icsStateHelper = new IcsStateHelper(sourceFolder);
				this.coreFolderReference = sourceFolder;
				this.coreFolderReference.AddRef();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0002068C File Offset: 0x0001E88C
		internal IcsStateHelper IcsStateHelper
		{
			get
			{
				base.CheckDisposed();
				return this.icsStateHelper;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0002069A File Offset: 0x0001E89A
		protected CoreFolder CoreFolder
		{
			get
			{
				base.CheckDisposed();
				return this.coreFolderReference.ReferencedObject;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000206AD File Offset: 0x0001E8AD
		public static void ValidateSourceKey(byte[] sourceKey, string parameterName)
		{
			Util.ThrowOnNullArgument(sourceKey, parameterName);
			if (sourceKey.Length != 22)
			{
				throw new RopExecutionException(string.Format("Invalid SourceKey[{0}] {1}.", parameterName, new ArrayTracer<byte>(sourceKey)), (ErrorCode)2147942487U);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000206D9 File Offset: 0x0001E8D9
		public static void ValidateChangeKey(byte[] changeKey, string parameterName)
		{
			Util.ThrowOnNullArgument(changeKey, parameterName);
			if (changeKey.Length < 17)
			{
				throw new RopExecutionException(string.Format("Invalid ChangeKey[{0}] {1}.", parameterName, new ArrayTracer<byte>(changeKey)), (ErrorCode)2147942487U);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00020705 File Offset: 0x0001E905
		IcsStateHelper IServiceProvider<IcsStateHelper>.Get()
		{
			return this.icsStateHelper;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0002070D File Offset: 0x0001E90D
		IFastTransferProcessor<FastTransferDownloadContext> IIcsStateCheckpoint.CreateIcsStateCheckpointFastTransferObject()
		{
			base.CheckDisposed();
			this.UpdateState();
			return this.icsStateHelper.CreateIcsStateFastTransferObject();
		}

		// Token: 0x06000495 RID: 1173
		internal abstract ErrorCode InternalImportDelete(ImportDeleteFlags importDeleteFlags, byte[][] sourceKeys);

		// Token: 0x06000496 RID: 1174 RVA: 0x00020728 File Offset: 0x0001E928
		internal ErrorCode ImportDelete(ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges)
		{
			Util.ThrowOnNullArgument(deleteChanges, "deleteChanges");
			RopHandler.CheckEnum<ImportDeleteFlags>(importDeleteFlags);
			if (deleteChanges.Length != 1)
			{
				throw new RopExecutionException("Can only pass one multi-valued property with IDs to delete.", (ErrorCode)2147942487U);
			}
			if (deleteChanges[0].PropertyTag.PropertyId != PropertyId.Null)
			{
				throw new RopExecutionException("Can only pass a specific property tag with IDs to delete.", (ErrorCode)2147942487U);
			}
			return this.InternalImportDelete(importDeleteFlags, deleteChanges[0].GetValue<byte[][]>());
		}

		// Token: 0x06000497 RID: 1175
		internal abstract void UpdateState();

		// Token: 0x06000498 RID: 1176 RVA: 0x00020798 File Offset: 0x0001E998
		protected static ErrorCode ConvertGroupOperationResultToErrorCode(OperationResult result)
		{
			EnumValidator.ThrowIfInvalid<OperationResult>(result);
			switch (result)
			{
			case OperationResult.Succeeded:
				return ErrorCode.None;
			case OperationResult.Failed:
				return (ErrorCode)2147500037U;
			case OperationResult.PartiallySucceeded:
				return ErrorCode.PartialCompletion;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000207D6 File Offset: 0x0001E9D6
		protected static DeleteItemFlags GetXsoDeleteItemFlags(ImportDeleteFlags importDeleteFlags)
		{
			if ((byte)(importDeleteFlags & ImportDeleteFlags.HardDelete) == 2)
			{
				return DeleteItemFlags.HardDelete;
			}
			return DeleteItemFlags.SoftDelete;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000207E2 File Offset: 0x0001E9E2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsUpload>(this);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000207EA File Offset: 0x0001E9EA
		protected override void InternalDispose()
		{
			ReferenceCount<CoreFolder>.ReleaseIfPresent(this.coreFolderReference);
			Util.DisposeIfPresent(this.icsStateHelper);
			base.InternalDispose();
		}

		// Token: 0x040001A9 RID: 425
		private const int SourceKeyLength = 22;

		// Token: 0x040001AA RID: 426
		private const int MinChangeKeyLength = 17;

		// Token: 0x040001AB RID: 427
		private readonly IcsStateHelper icsStateHelper;

		// Token: 0x040001AC RID: 428
		private readonly ReferenceCount<CoreFolder> coreFolderReference;
	}
}
