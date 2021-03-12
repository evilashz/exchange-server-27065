using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BinarySerializer : DisposeTrackableBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023BF File Offset: 0x000005BF
		public BinaryWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C7 File Offset: 0x000005C7
		public BinarySerializer()
		{
			this.stream = new MemoryStream();
			this.writer = new BinaryWriter(this.stream);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023EB File Offset: 0x000005EB
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.writer != null)
				{
					this.writer.Dispose();
					this.writer = null;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002424 File Offset: 0x00000624
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BinarySerializer>(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] ToArray()
		{
			return this.stream.ToArray();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002439 File Offset: 0x00000639
		public void Write(int value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002447 File Offset: 0x00000647
		public void Write(Guid value)
		{
			this.writer.Write(value.ToByteArray());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000245B File Offset: 0x0000065B
		public void Write(string value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002469 File Offset: 0x00000669
		public void Write(ulong value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002477 File Offset: 0x00000677
		public void Write(byte[] buffer)
		{
			this.Write(buffer.Length);
			this.writer.Write(buffer);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002490 File Offset: 0x00000690
		public void Write(PropValue pv)
		{
			this.Write((int)pv.PropTag);
			PropType propType = pv.PropTag.ValueType();
			if (propType <= PropType.String)
			{
				if (propType <= PropType.Boolean)
				{
					if (propType == PropType.Int)
					{
						this.Write(pv.GetInt());
						return;
					}
					switch (propType)
					{
					case PropType.Error:
						this.Write((int)pv.RawValue);
						return;
					case PropType.Boolean:
						this.Write(pv.GetBoolean() ? 1 : 0);
						return;
					}
				}
				else
				{
					if (propType == PropType.Long)
					{
						this.Write((ulong)pv.GetLong());
						return;
					}
					switch (propType)
					{
					case PropType.AnsiString:
					case PropType.String:
						this.Write(pv.GetString());
						return;
					}
				}
			}
			else if (propType <= PropType.Guid)
			{
				if (propType == PropType.SysTime)
				{
					this.Write((ulong)pv.GetLong());
					return;
				}
				if (propType == PropType.Guid)
				{
					this.Write(pv.GetGuid());
					return;
				}
			}
			else
			{
				if (propType == PropType.Binary)
				{
					this.Write(pv.GetBytes());
					return;
				}
				switch (propType)
				{
				case PropType.AnsiStringArray:
				case PropType.StringArray:
				{
					string[] stringArray = pv.GetStringArray();
					this.Write(stringArray.Length);
					foreach (string value in stringArray)
					{
						this.Write(value);
					}
					return;
				}
				}
			}
			MapiExceptionHelper.ThrowIfError(string.Format("Unable to serialize PropValue type {0}", pv.PropTag.ValueType()), -2147221246);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002608 File Offset: 0x00000808
		public void Write(PropValue[] pva)
		{
			this.Write(pva.Length);
			foreach (PropValue pv in pva)
			{
				this.Write(pv);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002644 File Offset: 0x00000844
		public static byte[] Serialize(BinarySerializer.SerializeDelegate del)
		{
			byte[] result;
			using (BinarySerializer binarySerializer = new BinarySerializer())
			{
				del(binarySerializer);
				result = binarySerializer.ToArray();
			}
			return result;
		}

		// Token: 0x04000019 RID: 25
		private MemoryStream stream;

		// Token: 0x0400001A RID: 26
		private BinaryWriter writer;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x0600001A RID: 26
		public delegate void SerializeDelegate(BinarySerializer serializer);
	}
}
