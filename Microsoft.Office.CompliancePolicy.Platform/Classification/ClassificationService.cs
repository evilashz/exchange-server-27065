using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000005 RID: 5
	public sealed class ClassificationService
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022DC File Offset: 0x000004DC
		public ClassificationService(IClassificationRuleStore classificationRuleStore, ClassificationConfiguration classificationConfiguration, ExecutionLog executionLog)
		{
			if (classificationRuleStore == null)
			{
				throw new ArgumentNullException("classificationRuleStore");
			}
			if (classificationConfiguration == null)
			{
				throw new ArgumentNullException("classificationConfiguration");
			}
			if (executionLog == null)
			{
				throw new ArgumentNullException("executionLog");
			}
			this.classificationRuleStore = classificationRuleStore;
			this.classificationConfiguration = classificationConfiguration;
			this.executionLog = executionLog;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002354 File Offset: 0x00000554
		private MicrosoftClassificationEngine ClassificationEngine
		{
			get
			{
				if (this.classificationEngine == null)
				{
					lock (this.lockObj)
					{
						if (this.classificationEngine == null)
						{
							this.executionLog.LogOneEntry(ExecutionLog.EventType.Information, "ClassificationService", this.correlationId, "Initializing ClassificationEngine", new object[0]);
							this.classificationEngine = new MicrosoftClassificationEngine();
							this.classificationEngine.Init(this.classificationConfiguration.PropertyBag, this.classificationRuleStore);
						}
					}
				}
				return this.classificationEngine;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023F0 File Offset: 0x000005F0
		public bool Classify(IClassificationItem classificationItem)
		{
			if (classificationItem == null)
			{
				this.executionLog.LogOneEntry(ExecutionLog.EventType.Verbose, "ClassificationService", this.correlationId, "Unable to classify: classificationItem is null", new object[0]);
				return false;
			}
			Stream content = classificationItem.Content;
			if (content == null)
			{
				this.executionLog.LogOneEntry(ExecutionLog.EventType.Verbose, "ClassificationService", this.correlationId, "Unable to classify [{0}]: no classificationItem doesn't have any content", new object[]
				{
					classificationItem.ItemId
				});
				return false;
			}
			RULE_PACKAGE_DETAILS[] rulePackageDetails = this.classificationRuleStore.GetRulePackageDetails(classificationItem);
			if (rulePackageDetails == null || rulePackageDetails.Length == 0)
			{
				this.executionLog.LogOneEntry(ExecutionLog.EventType.Verbose, "ClassificationService", this.correlationId, "Unable to classify [{0}]: no rules identified for classificationItem", new object[]
				{
					classificationItem.ItemId
				});
				return false;
			}
			this.executionLog.LogOneEntry(ExecutionLog.EventType.Verbose, "ClassificationService", this.correlationId, "Classifying [{0}]", new object[]
			{
				classificationItem.ItemId
			});
			ICAClassificationResultCollection classificationResults = this.ClassificationEngine.ClassifyTextStream(new ClassificationService.ReadOnlyNoSeekStreamWrapper(content), (uint)rulePackageDetails.Length, rulePackageDetails);
			this.executionLog.LogOneEntry(ExecutionLog.EventType.Verbose, "ClassificationService", this.correlationId, "Setting classification results for [{0}]", new object[]
			{
				classificationItem.ItemId
			});
			classificationItem.SetClassificationResults(classificationResults);
			return true;
		}

		// Token: 0x04000006 RID: 6
		public const string ConfidenceLevelResultPropertyName = "AFF85B32-1BA9-4EDE-9286-F08A7EE5A421";

		// Token: 0x04000007 RID: 7
		public const string CountResultPropertyName = "BD770258-EA9C-4162-B79C-7AD408EC7CD5";

		// Token: 0x04000008 RID: 8
		private const string ClientString = "ClassificationService";

		// Token: 0x04000009 RID: 9
		private readonly string correlationId = Guid.NewGuid().ToString();

		// Token: 0x0400000A RID: 10
		private object lockObj = new object();

		// Token: 0x0400000B RID: 11
		private ClassificationConfiguration classificationConfiguration;

		// Token: 0x0400000C RID: 12
		private IClassificationRuleStore classificationRuleStore;

		// Token: 0x0400000D RID: 13
		private ExecutionLog executionLog;

		// Token: 0x0400000E RID: 14
		private MicrosoftClassificationEngine classificationEngine;

		// Token: 0x02000006 RID: 6
		private class ReadOnlyNoSeekStreamWrapper : IStream
		{
			// Token: 0x0600001F RID: 31 RVA: 0x00002522 File Offset: 0x00000722
			internal ReadOnlyNoSeekStreamWrapper(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				if (!stream.CanRead)
				{
					throw new ArgumentException("stream argument must allow reads");
				}
				this.stream = stream;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002552 File Offset: 0x00000752
			public void Clone(out IStream ppstm)
			{
				ppstm = null;
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002561 File Offset: 0x00000761
			public void Commit(int grfCommitFlags)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002570 File Offset: 0x00000770
			public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				long num = 0L;
				long num2 = 0L;
				if (pstm != null)
				{
					int num3 = 0;
					IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(num3));
					byte[] array = new byte[8192];
					bool flag = false;
					while (!flag && cb > 0L)
					{
						int count = (cb > 8192L) ? 8192 : ((int)cb);
						int num4 = this.stream.Read(array, 0, count);
						num += (long)num4;
						if (num4 > 0)
						{
							pstm.Write(array, num4, intPtr);
							num3 = Marshal.ReadInt32(intPtr);
							num2 += (long)num3;
							cb -= (long)num4;
						}
						else
						{
							flag = true;
						}
					}
				}
				if (pcbRead != IntPtr.Zero)
				{
					Marshal.WriteInt64(pcbRead, num);
				}
				if (pcbWritten != IntPtr.Zero)
				{
					Marshal.WriteInt64(pcbWritten, num2);
				}
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002637 File Offset: 0x00000837
			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002644 File Offset: 0x00000844
			public void Read(byte[] pv, int cb, IntPtr pcbRead)
			{
				int val = this.stream.Read(pv, 0, cb);
				if (pcbRead != IntPtr.Zero)
				{
					Marshal.WriteInt32(pcbRead, val);
				}
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00002674 File Offset: 0x00000874
			public void Revert()
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002680 File Offset: 0x00000880
			public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000027 RID: 39 RVA: 0x0000268C File Offset: 0x0000088C
			public void SetSize(long libNewSize)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002698 File Offset: 0x00000898
			public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG
				{
					type = 2,
					cbSize = this.stream.Length,
					grfLocksSupported = 0,
					grfStateBits = 0
				};
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000026DE File Offset: 0x000008DE
			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000026EA File Offset: 0x000008EA
			public void Write(byte[] pv, int cb, IntPtr pcbWritten)
			{
				Marshal.ThrowExceptionForHR(-2147287039);
			}

			// Token: 0x0400000F RID: 15
			private const int INVALIDFUNCTION = -2147287039;

			// Token: 0x04000010 RID: 16
			private const int BufferSize = 8192;

			// Token: 0x04000011 RID: 17
			private Stream stream;
		}
	}
}
