using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200059B RID: 1435
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversionCallbackBase
	{
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000F17CF File Offset: 0x000EF9CF
		protected ConversionCallbackBase()
		{
			this.AttachmentCollection = null;
			this.itemBody = null;
			this.attachmentLinks = null;
			this.attachmentsByPositionEnumerator = null;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000F17FA File Offset: 0x000EF9FA
		protected ConversionCallbackBase(ICoreItem containerItem) : this(containerItem.AttachmentCollection, containerItem.Body)
		{
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000F180E File Offset: 0x000EFA0E
		protected ConversionCallbackBase(CoreAttachmentCollection attachmentCollection, Body itemBody)
		{
			this.AttachmentCollection = attachmentCollection;
			this.itemBody = itemBody;
			this.attachmentLinks = null;
			this.attachmentsByPositionEnumerator = null;
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000F1839 File Offset: 0x000EFA39
		public ReadOnlyCollection<AttachmentLink> AttachmentLinks
		{
			get
			{
				if (this.attachmentLinks == null)
				{
					this.InitializeAttachmentLinks(null);
				}
				return this.attachmentLinks;
			}
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000F1851 File Offset: 0x000EFA51
		public ReadOnlyCollection<AttachmentLink> InitializeAttachmentLinks(IList<AttachmentLink> linksToMerge)
		{
			this.attachmentLinks = AttachmentLink.MergeAttachmentLinks(linksToMerge, this.AttachmentCollection);
			return this.attachmentLinks;
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000F186B File Offset: 0x000EFA6B
		internal bool AttachmentListInitialized
		{
			get
			{
				return this.attachmentLinks != null;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000F1879 File Offset: 0x000EFA79
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000F1881 File Offset: 0x000EFA81
		public virtual CoreAttachmentCollection AttachmentCollection { get; private set; }

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000F188A File Offset: 0x000EFA8A
		public Body ItemBody
		{
			get
			{
				return this.itemBody;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000F1892 File Offset: 0x000EFA92
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000F189A File Offset: 0x000EFA9A
		public bool ClearInlineOnUnmarkedAttachments
		{
			get
			{
				return this.requireMarkInline;
			}
			set
			{
				this.requireMarkInline = value;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000F18A3 File Offset: 0x000EFAA3
		// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000F18AB File Offset: 0x000EFAAB
		public bool RemoveUnlinkedAttachments
		{
			get
			{
				return this.removeUnlinkedAttachments;
			}
			protected set
			{
				this.removeUnlinkedAttachments = value;
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F18B4 File Offset: 0x000EFAB4
		protected bool NeedsSave()
		{
			if (this.AttachmentCollection == null)
			{
				return false;
			}
			foreach (AttachmentLink attachmentLink in this.AttachmentLinks)
			{
				if (attachmentLink.NeedsSave(this.requireMarkInline))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F191C File Offset: 0x000EFB1C
		public virtual bool SaveChanges()
		{
			if (this.AttachmentCollection == null)
			{
				throw new InvalidOperationException("Target item not specified; callback cannot invoke attachment-specific methods");
			}
			bool result = false;
			if (this.attachmentLinks != null)
			{
				using (IEnumerator<AttachmentLink> enumerator = this.attachmentLinks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AttachmentLink attachmentLink = enumerator.Current;
						using (CoreAttachment coreAttachment = this.AttachmentCollection.Open(attachmentLink.AttachmentId))
						{
							using (Attachment attachment = Microsoft.Exchange.Data.Storage.AttachmentCollection.CreateTypedAttachment(coreAttachment, null))
							{
								if (this.removeUnlinkedAttachments && !attachmentLink.IsInline(this.requireMarkInline) && attachment.IsInline)
								{
									attachment.Dispose();
									this.AttachmentCollection.Remove(attachmentLink.AttachmentId);
								}
								else
								{
									if (attachmentLink.NeedsConversionToImage && attachment.AttachmentType == AttachmentType.Ole)
									{
										OleAttachment oleAttachment = attachment as OleAttachment;
										if (oleAttachment == null)
										{
											continue;
										}
										using (Attachment attachment2 = oleAttachment.ConvertToImageAttachment(this.AttachmentCollection, ImageFormat.Jpeg))
										{
											result = true;
											attachmentLink.MakeAttachmentChanges(attachment2, this.requireMarkInline);
											attachment2.Save();
											continue;
										}
									}
									if (attachmentLink.MakeAttachmentChanges(attachment, this.requireMarkInline))
									{
										result = true;
										attachment.Save();
									}
								}
							}
						}
					}
					return result;
				}
			}
			if (this.requireMarkInline)
			{
				List<AttachmentId> list = null;
				foreach (AttachmentHandle handle in this.AttachmentCollection)
				{
					using (CoreAttachment coreAttachment2 = this.AttachmentCollection.Open(handle))
					{
						if (coreAttachment2.IsInline)
						{
							if (this.removeUnlinkedAttachments)
							{
								if (list == null)
								{
									list = new List<AttachmentId>();
								}
								list.Add(coreAttachment2.Id);
							}
							else
							{
								coreAttachment2.IsInline = false;
								result = true;
								coreAttachment2.Save();
							}
						}
					}
				}
				if (list != null)
				{
					foreach (AttachmentId id in list)
					{
						this.AttachmentCollection.Remove(id);
					}
				}
			}
			return result;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000F1B8C File Offset: 0x000EFD8C
		private void CreateAttachmentsByPositionEnumerator()
		{
			List<KeyValuePair<int, AttachmentLink>> list = new List<KeyValuePair<int, AttachmentLink>>();
			foreach (AttachmentLink attachmentLink in this.AttachmentLinks)
			{
				if (attachmentLink.RenderingPosition >= 0)
				{
					list.Add(new KeyValuePair<int, AttachmentLink>(attachmentLink.RenderingPosition, attachmentLink));
				}
			}
			list.Sort(new ConversionCallbackBase.SortByAttachmentPositionComparer());
			this.attachmentsByPositionEnumerator = list.GetEnumerator();
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000F1C10 File Offset: 0x000EFE10
		public AttachmentLink FindAttachmentByIdOrContentId(AttachmentId attachmentId, string contentId)
		{
			Util.ThrowOnNullArgument(attachmentId, "attachmentId");
			if (this.AttachmentCollection == null)
			{
				throw new InvalidOperationException("Target item not specified; cannot invoke attachment-specific methods");
			}
			bool flag = string.IsNullOrEmpty(contentId);
			foreach (AttachmentLink attachmentLink in this.AttachmentLinks)
			{
				if (attachmentLink.AttachmentId.Equals(attachmentId) || (!flag && contentId.Equals(attachmentLink.ContentId, StringComparison.Ordinal)))
				{
					return attachmentLink;
				}
			}
			return null;
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000F1CA4 File Offset: 0x000EFEA4
		public AttachmentLink FindAttachmentByBodyReference(string bodyReference)
		{
			Util.ThrowOnNullArgument(bodyReference, "bodyReference");
			return this.InternalFindByBodyReference(bodyReference, null);
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000F1CB9 File Offset: 0x000EFEB9
		public AttachmentLink FindAttachmentByBodyReference(string bodyReference, Uri baseUri)
		{
			Util.ThrowOnNullArgument(bodyReference, "bodyReference");
			Util.ThrowOnNullArgument(baseUri, "baseUri");
			if (baseUri.IsWellFormedOriginalString())
			{
				return this.InternalFindByBodyReference(bodyReference, baseUri);
			}
			return this.InternalFindByBodyReference(bodyReference, null);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000F1CEC File Offset: 0x000EFEEC
		private AttachmentLink InternalFindByBodyReference(string bodyReference, Uri baseUri)
		{
			Uri uri;
			if (!Uri.TryCreate(bodyReference, UriKind.RelativeOrAbsolute, out uri))
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "BodyConversionCallbacksBase.InternalFindId: bodyReference not a valid URI\r\n'{0}'", bodyReference);
				return null;
			}
			if (!uri.IsWellFormedOriginalString())
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "BodyConversionCallbacksBase.InternalFindId: bodyReference not a valid URI\r\n'{0}'", bodyReference);
				return null;
			}
			if (uri.IsAbsoluteUri && uri.Scheme == "cid")
			{
				using (IEnumerator<AttachmentLink> enumerator = this.AttachmentLinks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AttachmentLink attachmentLink = enumerator.Current;
						AttachmentId attachmentId = attachmentLink.AttachmentId;
						string text;
						if (string.Equals(uri.LocalPath, attachmentLink.ContentId, StringComparison.OrdinalIgnoreCase))
						{
							text = attachmentLink.ContentId;
						}
						else if (string.Equals(uri.LocalPath, attachmentLink.Filename, StringComparison.OrdinalIgnoreCase))
						{
							text = attachmentLink.Filename;
						}
						else
						{
							if (!string.Equals(uri.LocalPath, attachmentLink.DisplayName, StringComparison.OrdinalIgnoreCase))
							{
								continue;
							}
							text = attachmentLink.DisplayName;
						}
						Uri contentLocation;
						if (!Uri.TryCreate("cid:" + text, UriKind.RelativeOrAbsolute, out contentLocation))
						{
							ExTraceGlobals.StorageTracer.TraceError<string, AttachmentId>((long)this.GetHashCode(), "BodyConversionCallbacksBase.InternalFindId: attachmentContentKey[{1}] not a valid URI\r\n'{0}'", text, attachmentId);
							return null;
						}
						if (!contentLocation.IsWellFormedOriginalString())
						{
							ExTraceGlobals.StorageTracer.TraceError<string, AttachmentId>((long)this.GetHashCode(), "BodyConversionCallbacksBase.InternalFindId: attachmentContentKey[{1}] not a valid URI\r\n'{0}'", text, attachmentId);
							return null;
						}
						return attachmentLink;
					}
					goto IL_2A1;
				}
			}
			if (uri.IsAbsoluteUri && uri.Scheme == "objattph")
			{
				if (this.attachmentsByPositionEnumerator == null)
				{
					this.CreateAttachmentsByPositionEnumerator();
				}
				if (this.attachmentsByPositionEnumerator.MoveNext())
				{
					KeyValuePair<int, AttachmentLink> keyValuePair = this.attachmentsByPositionEnumerator.Current;
					return keyValuePair.Value;
				}
				return null;
			}
			else
			{
				Uri uri2;
				if (uri.IsAbsoluteUri)
				{
					uri2 = uri;
				}
				else if (baseUri != null)
				{
					if (!Uri.TryCreate(baseUri, uri, out uri2))
					{
						ExTraceGlobals.StorageTracer.TraceError<Uri, Uri>((long)this.GetHashCode(), "AttachmentCollection.InternalFindId: can't build absolute URI from bodyReference and base\r\n'{0}'\r\n'{1}'", uri, baseUri);
						uri2 = uri;
					}
				}
				else
				{
					uri2 = uri;
				}
				foreach (AttachmentLink attachmentLink2 in this.AttachmentLinks)
				{
					AttachmentId attachmentId2 = attachmentLink2.AttachmentId;
					Uri contentLocation = attachmentLink2.ContentLocation;
					Uri contentBase = attachmentLink2.ContentBase;
					if (!(contentLocation == null))
					{
						Uri obj;
						if (contentLocation.IsAbsoluteUri)
						{
							obj = contentLocation;
						}
						else if (contentBase != null)
						{
							if (!Uri.TryCreate(contentBase, contentLocation, out obj))
							{
								ExTraceGlobals.StorageTracer.TraceError<Uri, AttachmentId>((long)this.GetHashCode(), "AttachmentCollection.InternalFindId: attachContentLocation[{1}] not a valid URI\r\n'{0}'", contentLocation, attachmentId2);
								obj = contentLocation;
							}
						}
						else
						{
							obj = contentLocation;
						}
						try
						{
							if (uri2.Equals(obj))
							{
								return attachmentLink2;
							}
						}
						catch (UriFormatException)
						{
						}
					}
				}
			}
			IL_2A1:
			return null;
		}

		// Token: 0x04001F7C RID: 8060
		private ReadOnlyCollection<AttachmentLink> attachmentLinks;

		// Token: 0x04001F7D RID: 8061
		private Body itemBody;

		// Token: 0x04001F7E RID: 8062
		private IEnumerator<KeyValuePair<int, AttachmentLink>> attachmentsByPositionEnumerator;

		// Token: 0x04001F7F RID: 8063
		private bool requireMarkInline = true;

		// Token: 0x04001F80 RID: 8064
		private bool removeUnlinkedAttachments;

		// Token: 0x0200059C RID: 1436
		private class SortByAttachmentPositionComparer : IComparer<KeyValuePair<int, AttachmentLink>>
		{
			// Token: 0x06003ACD RID: 15053 RVA: 0x000F1FC8 File Offset: 0x000F01C8
			public int Compare(KeyValuePair<int, AttachmentLink> lhs, KeyValuePair<int, AttachmentLink> rhs)
			{
				return lhs.Key.CompareTo(rhs.Key);
			}
		}
	}
}
