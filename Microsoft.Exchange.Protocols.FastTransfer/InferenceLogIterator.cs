using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000019 RID: 25
	internal class InferenceLogIterator : DisposableBase, IMessageIterator, IMessageIteratorClient, IDisposable
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007956 File Offset: 0x00005B56
		public InferenceLogIterator(FastTransferDownloadContext downloadContext)
		{
			this.context = downloadContext;
			this.readOnly = true;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000796C File Offset: 0x00005B6C
		public InferenceLogIterator(FastTransferUploadContext uploadContext)
		{
			this.context = uploadContext;
			this.readOnly = false;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00007982 File Offset: 0x00005B82
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000798A File Offset: 0x00005B8A
		public FastTransferContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007D58 File Offset: 0x00005F58
		public IEnumerator<IMessage> GetMessages()
		{
			InferenceLogViewTable logView = new InferenceLogViewTable(this.Context.CurrentOperationContext, this.Context.Logon.StoreMailbox);
			List<Properties> cachedRows = new List<Properties>(32);
			long currentItemCount = 0L;
			bool stopConditionHit = false;
			DateTime stopCreateTime = DateTime.UtcNow;
			UnlimitedItems stopItemCount = ConfigurationSchema.InferenceLogMaxRows.Value;
			if (ConfigurationSchema.InferenceLogRetentionPeriod.Value > stopCreateTime - DateTime.MinValue)
			{
				stopCreateTime = DateTime.MinValue;
			}
			else
			{
				stopCreateTime -= ConfigurationSchema.InferenceLogRetentionPeriod.Value;
			}
			logView.SeekRow(this.Context.CurrentOperationContext, ViewSeekOrigin.End, 0);
			while (!stopConditionHit)
			{
				using (Reader reader = logView.QueryRows(this.Context.CurrentOperationContext, 32, true))
				{
					if (reader == null)
					{
						yield break;
					}
					while (reader.Read())
					{
						using (InferenceLog inferenceLog = InferenceLog.Open(this.Context.CurrentOperationContext, this.Context.Logon.StoreMailbox, reader))
						{
							Properties rowProps = new Properties(25);
							inferenceLog.EnumerateProperties(this.Context.CurrentOperationContext, delegate(StorePropTag tag, object value)
							{
								rowProps.Add(tag, value);
								return true;
							}, true);
							currentItemCount += 1L;
							if (!stopItemCount.IsUnlimited && stopItemCount.Value < currentItemCount)
							{
								stopConditionHit = true;
								break;
							}
							DateTime t = (DateTime)inferenceLog.GetPropertyValue(this.Context.CurrentOperationContext, PropTag.InferenceLog.InferenceTimeStamp);
							if (t < stopCreateTime)
							{
								stopConditionHit = true;
								break;
							}
							cachedRows.Add(rowProps);
							logView.BookmarkCurrentRow(reader, true);
						}
					}
				}
				if (cachedRows.Count == 0)
				{
					break;
				}
				foreach (Properties row in cachedRows)
				{
					yield return new InferenceLogIterator.Record(this, row);
				}
				cachedRows.Clear();
			}
			yield break;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007D74 File Offset: 0x00005F74
		public IMessage UploadMessage(bool isAssociatedMessage)
		{
			return new InferenceLogIterator.Record(this, Properties.Empty);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007D81 File Offset: 0x00005F81
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InferenceLogIterator>(this);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007D89 File Offset: 0x00005F89
		protected override void InternalDispose(bool isCalledFromDispose)
		{
		}

		// Token: 0x04000078 RID: 120
		public const int MaximumRowsPerBatch = 32;

		// Token: 0x04000079 RID: 121
		private const int RowCapacity = 25;

		// Token: 0x0400007A RID: 122
		private readonly bool readOnly;

		// Token: 0x0400007B RID: 123
		private FastTransferContext context;

		// Token: 0x0200001A RID: 26
		private class Record : IMessage, IDisposable
		{
			// Token: 0x06000100 RID: 256 RVA: 0x00007D8C File Offset: 0x00005F8C
			public Record(InferenceLogIterator logIterator, Properties properties)
			{
				this.logIterator = logIterator;
				this.propertyBag = new MemoryPropertyBag(logIterator.Context);
				if (properties.Count > 0)
				{
					foreach (Property prop in properties)
					{
						PropertyValue property = RcaTypeHelpers.MassageOutgoingProperty(prop, true);
						if (property.PropertyTag.PropertyType != PropertyType.Error)
						{
							this.propertyBag.SetProperty(property);
						}
					}
					this.propertyBag.SetProperty(new PropertyValue(PropertyTag.Mid, 0L));
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00007E40 File Offset: 0x00006040
			public IPropertyBag PropertyBag
			{
				get
				{
					return this.propertyBag;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00007E48 File Offset: 0x00006048
			public bool IsAssociated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00007EEC File Offset: 0x000060EC
			public IEnumerable<IRecipient> GetRecipients()
			{
				yield break;
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00007F09 File Offset: 0x00006109
			public IRecipient CreateRecipient()
			{
				throw new ExExceptionNoSupport((LID)61264U, "Recipients are not supported on the log records");
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00007F1F File Offset: 0x0000611F
			public void RemoveRecipient(int rowId)
			{
				throw new ExExceptionNoSupport((LID)38364U, "Recipient removal is not supported on the log records");
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00007FD8 File Offset: 0x000061D8
			public IEnumerable<IAttachmentHandle> GetAttachments()
			{
				yield break;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00007FF5 File Offset: 0x000061F5
			public IAttachment CreateAttachment()
			{
				throw new ExExceptionNoSupport((LID)44880U, "Attachments are not supported on the log records");
			}

			// Token: 0x06000108 RID: 264 RVA: 0x0000800C File Offset: 0x0000620C
			public void Save()
			{
				using (InferenceLog inferenceLog = InferenceLog.Create(this.logIterator.Context.CurrentOperationContext, this.logIterator.Context.Logon.StoreMailbox))
				{
					bool flag = false;
					foreach (AnnotatedPropertyValue annotatedPropertyValue in this.PropertyBag.GetAnnotatedProperties())
					{
						if (!annotatedPropertyValue.PropertyValue.IsError)
						{
							StorePropTag storePropTag = LegacyHelper.ConvertFromLegacyPropTag(annotatedPropertyValue.PropertyValue.PropertyTag, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.InferenceLog, this.logIterator.Context.Logon.MapiMailbox, true);
							if (storePropTag != PropTag.InferenceLog.RowId && storePropTag != PropTag.InferenceLog.Mid)
							{
								object value = annotatedPropertyValue.PropertyValue.Value;
								RcaTypeHelpers.MassageIncomingPropertyValue(annotatedPropertyValue.PropertyValue.PropertyTag, ref value);
								ErrorCode errorCode = inferenceLog.SetProperty(this.logIterator.Context.CurrentOperationContext, storePropTag, value);
								if (errorCode != ErrorCode.NoError)
								{
									throw new StoreException((LID)57168U, errorCode, "Unable to set property on inference log record");
								}
								flag = true;
							}
						}
					}
					if (flag)
					{
						inferenceLog.Flush(this.logIterator.Context.CurrentOperationContext);
					}
				}
			}

			// Token: 0x06000109 RID: 265 RVA: 0x000081AC File Offset: 0x000063AC
			public void SetLongTermId(StoreLongTermId longTermId)
			{
			}

			// Token: 0x0600010A RID: 266 RVA: 0x000081AE File Offset: 0x000063AE
			public void Dispose()
			{
			}

			// Token: 0x0400007C RID: 124
			private InferenceLogIterator logIterator;

			// Token: 0x0400007D RID: 125
			private MemoryPropertyBag propertyBag;
		}
	}
}
