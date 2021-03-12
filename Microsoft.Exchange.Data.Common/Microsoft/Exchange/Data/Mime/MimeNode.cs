using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200001D RID: 29
	public abstract class MimeNode : IEnumerable<MimeNode>, IEnumerable
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000528D File Offset: 0x0000348D
		internal MimeNode(MimeNode parent)
		{
			this.parentNode = parent;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000529C File Offset: 0x0000349C
		internal MimeNode()
		{
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000052A4 File Offset: 0x000034A4
		public bool HasChildren
		{
			get
			{
				return null != this.FirstChild;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000052B2 File Offset: 0x000034B2
		public MimeNode Parent
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000052BA File Offset: 0x000034BA
		public MimeNode FirstChild
		{
			get
			{
				if (this.lastChild == null)
				{
					return this.ParseNextChild();
				}
				return this.lastChild.nextSibling;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000052D6 File Offset: 0x000034D6
		public MimeNode LastChild
		{
			get
			{
				while (this.ParseNextChild() != null)
				{
				}
				return this.lastChild;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000052E6 File Offset: 0x000034E6
		public MimeNode NextSibling
		{
			get
			{
				if (this.parentNode == null)
				{
					return null;
				}
				if (this != this.parentNode.lastChild)
				{
					return this.nextSibling;
				}
				return this.parentNode.ParseNextChild();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005314 File Offset: 0x00003514
		public MimeNode PreviousSibling
		{
			get
			{
				if (this.parentNode == null || this.parentNode.lastChild.nextSibling == this)
				{
					return null;
				}
				MimeNode mimeNode = this.parentNode.lastChild.nextSibling;
				int num = 0;
				while (mimeNode.nextSibling != this)
				{
					num++;
					this.CheckLoopCount(num);
					mimeNode = mimeNode.nextSibling;
				}
				return mimeNode;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000536F File Offset: 0x0000356F
		internal MimeNode InternalLastChild
		{
			get
			{
				return this.lastChild;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005377 File Offset: 0x00003577
		internal MimeNode InternalNextSibling
		{
			get
			{
				if (this.parentNode == null || this == this.parentNode.lastChild)
				{
					return null;
				}
				return this.nextSibling;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005398 File Offset: 0x00003598
		public MimeNode InsertBefore(MimeNode newChild, MimeNode refChild)
		{
			this.ThrowIfReadOnly("MimeNode.InsertBefore");
			if (refChild == null)
			{
				refChild = this.LastChild;
			}
			else if (this.lastChild != null && refChild == this.lastChild.nextSibling)
			{
				refChild = null;
			}
			else
			{
				refChild = refChild.PreviousSibling;
				if (refChild == null)
				{
					throw new ArgumentException(Strings.RefChildIsNotMyChild, "refChild");
				}
			}
			return this.InsertAfter(newChild, refChild);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000053FD File Offset: 0x000035FD
		public MimeNode InsertAfter(MimeNode newChild, MimeNode refChild)
		{
			this.ThrowIfReadOnly("MimeNode.InsertAfter");
			newChild = this.InternalInsertAfter(newChild, refChild);
			this.SetDirty();
			return newChild;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000541B File Offset: 0x0000361B
		public MimeNode AppendChild(MimeNode newChild)
		{
			this.ThrowIfReadOnly("MimeNode.AppendChild");
			return this.InsertAfter(newChild, this.LastChild);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005435 File Offset: 0x00003635
		public MimeNode PrependChild(MimeNode newChild)
		{
			this.ThrowIfReadOnly("MimeNode.PrependChild");
			return this.InsertAfter(newChild, null);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000544A File Offset: 0x0000364A
		public MimeNode RemoveChild(MimeNode oldChild)
		{
			this.ThrowIfReadOnly("MimeNode.RemoveChild");
			oldChild = this.InternalRemoveChild(oldChild);
			this.SetDirty();
			return oldChild;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005468 File Offset: 0x00003668
		public MimeNode ReplaceChild(MimeNode newChild, MimeNode oldChild)
		{
			this.ThrowIfReadOnly("MimeNode.ReplaceChild");
			if (oldChild == null)
			{
				throw new ArgumentNullException("oldChild");
			}
			if (this != oldChild.parentNode)
			{
				throw new ArgumentException(Strings.OldChildIsNotMyChild, "oldChild");
			}
			if (newChild == oldChild)
			{
				return oldChild;
			}
			MimeNode result = this.InsertAfter(newChild, oldChild);
			if (this == oldChild.parentNode)
			{
				this.RemoveChild(oldChild);
			}
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000054C8 File Offset: 0x000036C8
		public void RemoveAll()
		{
			this.ThrowIfReadOnly("MimeNode.RemoveAll");
			this.InternalRemoveAll();
			this.SetDirty();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000054E1 File Offset: 0x000036E1
		internal virtual void RemoveAllUnparsed()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000054E3 File Offset: 0x000036E3
		public void RemoveFromParent()
		{
			this.ThrowIfReadOnly("MimeNode.RemoveFromParent");
			if (this.parentNode != null)
			{
				this.parentNode.RemoveChild(this);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005505 File Offset: 0x00003705
		public long WriteTo(Stream stream)
		{
			return this.WriteTo(stream, null);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005510 File Offset: 0x00003710
		public long WriteTo(Stream stream, EncodingOptions encodingOptions)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encodingOptions == null)
			{
				encodingOptions = this.GetDocumentEncodingOptions();
			}
			byte[] array = null;
			MimeStringLength mimeStringLength = new MimeStringLength(0);
			return this.WriteTo(stream, encodingOptions, null, ref mimeStringLength, ref array);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005552 File Offset: 0x00003752
		public void WriteTo(MimeWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteMimeNode(this);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005569 File Offset: 0x00003769
		public MimeNode.Enumerator<MimeNode> GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeNode>(this);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005571 File Offset: 0x00003771
		IEnumerator<MimeNode> IEnumerable<MimeNode>.GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeNode>(this);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000557E File Offset: 0x0000377E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeNode>(this);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000558B File Offset: 0x0000378B
		public virtual MimeNode Clone()
		{
			throw new NotSupportedException(Strings.ThisNodeDoesNotSupportCloning(base.GetType().ToString()));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000055A4 File Offset: 0x000037A4
		public virtual void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			MimeNode mimeNode = destination as MimeNode;
			if (mimeNode == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			mimeNode.RemoveAll();
			if (this.lastChild != null)
			{
				for (MimeNode internalNextSibling = this.lastChild.nextSibling; internalNextSibling != null; internalNextSibling = internalNextSibling.InternalNextSibling)
				{
					mimeNode.InternalAppendChild(internalNextSibling.Clone());
				}
			}
			mimeNode.SetDirty();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005610 File Offset: 0x00003810
		internal MimeNode InternalInsertAfter(MimeNode newChild, MimeNode refChild)
		{
			this.ThrowIfReadOnly("MimeNode.InternalInsertAfter");
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (refChild != null)
			{
				if (refChild == newChild)
				{
					return refChild;
				}
				if (this != refChild.parentNode)
				{
					throw new ArgumentException(Strings.RefChildIsNotMyChild, "refChild");
				}
			}
			if (newChild.parentNode != null)
			{
				throw new ArgumentException(Strings.NewChildCannotHaveDifferentParent);
			}
			refChild = this.ValidateNewChild(newChild, refChild);
			newChild.parentNode = this;
			if (refChild == null)
			{
				if (this.lastChild == null)
				{
					newChild.nextSibling = newChild;
					this.lastChild = newChild;
				}
				else
				{
					newChild.nextSibling = this.lastChild.nextSibling;
					this.lastChild.nextSibling = newChild;
				}
			}
			else
			{
				newChild.nextSibling = refChild.nextSibling;
				refChild.nextSibling = newChild;
				if (refChild == this.lastChild)
				{
					this.lastChild = newChild;
				}
			}
			return newChild;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000056DA File Offset: 0x000038DA
		internal MimeNode InternalAppendChild(MimeNode newChild)
		{
			this.ThrowIfReadOnly("MimeNode.InternalAppendChild");
			return this.InternalInsertAfter(newChild, this.LastChild);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000056F4 File Offset: 0x000038F4
		internal MimeNode InternalRemoveChild(MimeNode oldChild)
		{
			this.ThrowIfReadOnly("MimeNode.InternalRemoveChild");
			if (oldChild == null)
			{
				throw new ArgumentNullException("oldChild");
			}
			if (this != oldChild.parentNode)
			{
				throw new ArgumentException(Strings.OldChildIsNotMyChild, "oldChild");
			}
			if (oldChild == this.lastChild.nextSibling)
			{
				if (oldChild == this.lastChild)
				{
					this.lastChild = null;
				}
				else
				{
					this.lastChild.nextSibling = oldChild.nextSibling;
				}
			}
			else
			{
				MimeNode previousSibling = oldChild.PreviousSibling;
				previousSibling.nextSibling = oldChild.nextSibling;
				if (oldChild == this.lastChild)
				{
					this.lastChild = previousSibling;
				}
			}
			oldChild.parentNode = null;
			oldChild.nextSibling = null;
			this.ChildRemoved(oldChild);
			return oldChild;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000057A0 File Offset: 0x000039A0
		internal void InternalRemoveAll()
		{
			this.ThrowIfReadOnly("MimeNode.InternalRemoveAll");
			while (this.lastChild != null)
			{
				MimeNode mimeNode = this.lastChild.nextSibling;
				if (mimeNode == this.lastChild)
				{
					this.lastChild = null;
				}
				else
				{
					this.lastChild.nextSibling = mimeNode.nextSibling;
				}
				mimeNode.nextSibling = null;
				mimeNode.parentNode = null;
				this.ChildRemoved(mimeNode);
			}
			this.RemoveAllUnparsed();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000580C File Offset: 0x00003A0C
		internal void InternalDetachParent()
		{
			this.parentNode = null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005815 File Offset: 0x00003A15
		internal virtual MimeNode ParseNextChild()
		{
			return null;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005818 File Offset: 0x00003A18
		internal virtual void CheckChildrenLimit(int countLimit, int bytesLimit)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000581A File Offset: 0x00003A1A
		internal virtual MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			return refChild;
		}

		// Token: 0x06000132 RID: 306
		internal abstract long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer);

		// Token: 0x06000133 RID: 307 RVA: 0x0000581D File Offset: 0x00003A1D
		internal virtual void ChildRemoved(MimeNode oldChild)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000581F File Offset: 0x00003A1F
		internal virtual void SetDirty()
		{
			this.ThrowIfReadOnly("MimeNode.SetDirty");
			if (this.parentNode != null)
			{
				this.parentNode.SetDirty();
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005840 File Offset: 0x00003A40
		internal EncodingOptions GetDocumentEncodingOptions()
		{
			MimeDocument mimeDocument;
			MimeNode mimeNode;
			this.GetMimeDocumentOrTreeRoot(out mimeDocument, out mimeNode);
			if (mimeDocument != null)
			{
				return mimeDocument.EncodingOptions;
			}
			MimePart mimePart = mimeNode as MimePart;
			return new EncodingOptions((mimePart == null) ? null : mimePart.FindMimeTreeCharset());
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000587C File Offset: 0x00003A7C
		internal DecodingOptions GetHeaderDecodingOptions()
		{
			MimeDocument mimeDocument;
			MimeNode mimeNode;
			this.GetMimeDocumentOrTreeRoot(out mimeDocument, out mimeNode);
			DecodingOptions result;
			if (mimeDocument != null)
			{
				result = mimeDocument.EffectiveHeaderDecodingOptions;
				if (result.Charset == null)
				{
					result.Charset = DecodingOptions.DefaultCharset;
				}
			}
			else
			{
				result = DecodingOptions.Default;
				result.Charset = mimeNode.GetDefaultHeaderDecodingCharset(null, mimeNode);
			}
			return result;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000058CC File Offset: 0x00003ACC
		internal Charset GetDefaultHeaderDecodingCharset(MimeDocument document, MimeNode treeRoot)
		{
			if (treeRoot == null)
			{
				this.GetMimeDocumentOrTreeRoot(out document, out treeRoot);
			}
			Charset charset;
			if (document != null)
			{
				charset = document.EffectiveHeaderDecodingOptions.Charset;
			}
			else
			{
				MimePart mimePart = treeRoot as MimePart;
				charset = ((mimePart == null) ? null : mimePart.FindMimeTreeCharset());
			}
			if (charset == null)
			{
				charset = DecodingOptions.DefaultCharset;
			}
			return charset;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000591C File Offset: 0x00003B1C
		internal MimeNode GetTreeRoot()
		{
			MimeNode mimeNode = this;
			while (mimeNode.parentNode != null)
			{
				mimeNode = mimeNode.parentNode;
			}
			return mimeNode;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000593D File Offset: 0x00003B3D
		internal void GetMimeDocumentOrTreeRoot(out MimeDocument document, out MimeNode treeRoot)
		{
			document = MimeNode.GetParentDocument(this, out treeRoot);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005948 File Offset: 0x00003B48
		protected static MimeDocument GetParentDocument(MimeNode node, out MimeNode treeRoot)
		{
			treeRoot = node.GetTreeRoot();
			MimePart mimePart = treeRoot as MimePart;
			if (mimePart != null)
			{
				return mimePart.ParentDocument;
			}
			return null;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005970 File Offset: 0x00003B70
		protected bool IsReadOnly
		{
			get
			{
				MimeNode mimeNode;
				MimeDocument parentDocument = MimeNode.GetParentDocument(this, out mimeNode);
				return parentDocument != null && parentDocument.IsReadOnly;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005991 File Offset: 0x00003B91
		protected void ThrowIfReadOnly(string method)
		{
			if (this.IsReadOnly)
			{
				throw new ReadOnlyMimeException(method);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000059A4 File Offset: 0x00003BA4
		private void CheckLoopCount(int count)
		{
			if (!this.loopLimitInitialized)
			{
				MimeLimits mimeLimits = MimeLimits.Default;
				MimeDocument mimeDocument;
				MimeNode mimeNode;
				this.GetMimeDocumentOrTreeRoot(out mimeDocument, out mimeNode);
				if (mimeDocument != null)
				{
					mimeLimits = mimeDocument.MimeLimits;
				}
				if (this is Header)
				{
					this.loopLimit = mimeLimits.MaxHeaders;
				}
				else if (this is AddressItem)
				{
					this.loopLimit = mimeLimits.MaxAddressItemsPerHeader;
				}
				else if (this is MimeParameter)
				{
					this.loopLimit = mimeLimits.MaxParametersPerHeader;
				}
				else
				{
					this.loopLimit = mimeLimits.MaxParts;
				}
				this.loopLimitInitialized = true;
			}
			if (count > this.loopLimit)
			{
				string message = string.Format("Loop detected in sibling collection. Loop count: {0}", this.loopLimit);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x040000BF RID: 191
		private const string LoopLimitMessage = "Loop detected in sibling collection. Loop count: {0}";

		// Token: 0x040000C0 RID: 192
		private bool loopLimitInitialized;

		// Token: 0x040000C1 RID: 193
		private int loopLimit;

		// Token: 0x040000C2 RID: 194
		private MimeNode parentNode;

		// Token: 0x040000C3 RID: 195
		private MimeNode nextSibling;

		// Token: 0x040000C4 RID: 196
		private MimeNode lastChild;

		// Token: 0x0200001E RID: 30
		public struct Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator where T : MimeNode
		{
			// Token: 0x0600013E RID: 318 RVA: 0x00005A4E File Offset: 0x00003C4E
			internal Enumerator(MimeNode node)
			{
				this.node = node;
				this.current = default(T);
				this.next = (this.node.FirstChild as T);
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00005A7E File Offset: 0x00003C7E
			object IEnumerator.Current
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException((this.next == null) ? Strings.ErrorAfterLast : Strings.ErrorBeforeFirst);
					}
					return this.current;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000140 RID: 320 RVA: 0x00005AB7 File Offset: 0x00003CB7
			public T Current
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException((this.next == null) ? Strings.ErrorAfterLast : Strings.ErrorBeforeFirst);
					}
					return this.current;
				}
			}

			// Token: 0x06000141 RID: 321 RVA: 0x00005AEB File Offset: 0x00003CEB
			public bool MoveNext()
			{
				this.current = this.next;
				if (this.current == null)
				{
					return false;
				}
				this.next = (this.current.NextSibling as T);
				return true;
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00005B2A File Offset: 0x00003D2A
			public void Reset()
			{
				this.current = default(T);
				this.next = (this.node.FirstChild as T);
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00005B53 File Offset: 0x00003D53
			public void Dispose()
			{
				this.Reset();
			}

			// Token: 0x040000C5 RID: 197
			private MimeNode node;

			// Token: 0x040000C6 RID: 198
			private T current;

			// Token: 0x040000C7 RID: 199
			private T next;
		}
	}
}
