using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000187 RID: 391
	internal class MessagePlayerContext
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000322E8 File Offset: 0x000304E8
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x000322F0 File Offset: 0x000304F0
		internal StoreObjectId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x000322F9 File Offset: 0x000304F9
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00032301 File Offset: 0x00030501
		internal LinkedListNode<MessagePartManager.MessagePart> CurrentTextPart
		{
			get
			{
				return this.currentTextPart;
			}
			set
			{
				this.currentTextPart = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0003230A File Offset: 0x0003050A
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00032312 File Offset: 0x00030512
		internal LinkedListNode<MessagePartManager.MessagePart> CurrentWavePart
		{
			get
			{
				return this.currentWavePart;
			}
			set
			{
				this.currentWavePart = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0003231B File Offset: 0x0003051B
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x00032323 File Offset: 0x00030523
		internal char[] Remnant
		{
			get
			{
				return this.remnant;
			}
			set
			{
				this.remnant = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0003232C File Offset: 0x0003052C
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x00032334 File Offset: 0x00030534
		internal PlaybackMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0003233D File Offset: 0x0003053D
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00032345 File Offset: 0x00030545
		internal long SeekPosition
		{
			get
			{
				return this.seekPosition;
			}
			set
			{
				this.seekPosition = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003234E File Offset: 0x0003054E
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x00032356 File Offset: 0x00030556
		internal PlaybackContent ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003235F File Offset: 0x0003055F
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00032367 File Offset: 0x00030567
		internal CultureInfo Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00032370 File Offset: 0x00030570
		internal void Reset(StoreObjectId id)
		{
			this.Id = id;
			this.CurrentTextPart = null;
			this.CurrentWavePart = null;
			this.Remnant = new char[0];
			this.SeekPosition = 0L;
			this.Mode = PlaybackMode.None;
			this.ContentType = PlaybackContent.Unknown;
			this.Language = null;
		}

		// Token: 0x040009D2 RID: 2514
		private StoreObjectId id;

		// Token: 0x040009D3 RID: 2515
		private LinkedListNode<MessagePartManager.MessagePart> currentTextPart;

		// Token: 0x040009D4 RID: 2516
		private LinkedListNode<MessagePartManager.MessagePart> currentWavePart;

		// Token: 0x040009D5 RID: 2517
		private char[] remnant;

		// Token: 0x040009D6 RID: 2518
		private long seekPosition;

		// Token: 0x040009D7 RID: 2519
		private PlaybackMode mode;

		// Token: 0x040009D8 RID: 2520
		private PlaybackContent contentType;

		// Token: 0x040009D9 RID: 2521
		private CultureInfo language;
	}
}
