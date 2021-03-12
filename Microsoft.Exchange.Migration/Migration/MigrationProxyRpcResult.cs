using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationProxyRpcResult
	{
		// Token: 0x06000BED RID: 3053 RVA: 0x000344D1 File Offset: 0x000326D1
		protected MigrationProxyRpcResult(MigrationProxyRpcType type)
		{
			this.Type = type;
			this.PropertyCollection = new MdbefPropertyCollection();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000344EB File Offset: 0x000326EB
		protected MigrationProxyRpcResult(byte[] resultBlob, MigrationProxyRpcType type)
		{
			MigrationUtil.ThrowOnNullArgument(resultBlob, "resultBlob");
			this.Type = type;
			this.PropertyCollection = MdbefPropertyCollection.Create(resultBlob, 0, resultBlob.Length);
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00034518 File Offset: 0x00032718
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00034541 File Offset: 0x00032741
		public string ErrorMessage
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2432761887U, out obj))
				{
					return obj as string;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2432761887U] = value;
					return;
				}
				this.PropertyCollection.Remove(2432761887U);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00034570 File Offset: 0x00032770
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x000345A1 File Offset: 0x000327A1
		public int RpcErrorCode
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433548291U, out obj) && obj is int)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value != 0)
				{
					this.PropertyCollection[2433548291U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433548291U);
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000345CE File Offset: 0x000327CE
		public byte[] GetBytes()
		{
			return this.PropertyCollection.GetBytes();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000345DC File Offset: 0x000327DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<Response Type='{0}'>", this.Type);
			foreach (KeyValuePair<uint, object> keyValuePair in this.PropertyCollection)
			{
				uint key = keyValuePair.Key;
				if (key <= 2433089567U)
				{
					if (key <= 2432892959U)
					{
						if (key == 2432761887U)
						{
							stringBuilder.AppendFormat("<Result Key='Exception' Value='{0}' />", keyValuePair.Value);
							continue;
						}
						if (key == 2432827395U)
						{
							stringBuilder.AppendFormat("<Result Key='NSPI Total Size' Value='{0}' />", keyValuePair.Value);
							continue;
						}
						if (key == 2432892959U)
						{
							stringBuilder.AppendFormat("<Result Key='NSPI Server' Value='{0}' />", keyValuePair.Value);
							continue;
						}
					}
					else
					{
						if (key == 2432958467U)
						{
							stringBuilder.AppendFormat("<Result Key='Autodiscover Status' Value='{0}' />", keyValuePair.Value);
							continue;
						}
						if (key == 2433024003U)
						{
							stringBuilder.AppendFormat("<Result Key='Autodiscover Exchange Version' Value='{0}' />", keyValuePair.Value);
							continue;
						}
						if (key == 2433089567U)
						{
							stringBuilder.AppendFormat("<Result Key='Autodiscover Mailbox DN' Value='{0}' />", keyValuePair.Value);
							continue;
						}
					}
				}
				else if (key <= 2433286175U)
				{
					if (key == 2433155103U)
					{
						stringBuilder.AppendFormat("<Result Key='Autodiscover Exchange Server DN' Value='{0}' />", keyValuePair.Value);
						continue;
					}
					if (key == 2433220639U)
					{
						stringBuilder.AppendFormat("<Result Key='Http Proxy Server' Value='{0}' />", keyValuePair.Value);
						continue;
					}
					if (key == 2433286175U)
					{
						stringBuilder.AppendFormat("<Result Key='Autodiscover Exchange Server' Value='{0}' />", keyValuePair.Value);
						continue;
					}
				}
				else if (key <= 2433417247U)
				{
					if (key == 2433351683U)
					{
						stringBuilder.AppendFormat("<Result Key='Autodiscover Authentication Method' Value='{0}' />", keyValuePair.Value);
						continue;
					}
					if (key == 2433417247U)
					{
						stringBuilder.AppendFormat("<Result Key='Autodiscover Url' Value='{0}' />", keyValuePair.Value);
						continue;
					}
				}
				else
				{
					if (key == 2433482755U)
					{
						stringBuilder.AppendFormat("<Result Key='Autodiscover Error Code' Value='{0}' />", keyValuePair.Value);
						continue;
					}
					if (key == 2433548291U)
					{
						stringBuilder.AppendFormat("<Result Key='RpcErrorCode' Value='{0}' />", keyValuePair.Value);
						continue;
					}
				}
				stringBuilder.AppendFormat("<InvalidResult/>", new object[0]);
			}
			stringBuilder.AppendFormat("</Response>", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x0400049B RID: 1179
		public readonly MigrationProxyRpcType Type;

		// Token: 0x0400049C RID: 1180
		protected readonly MdbefPropertyCollection PropertyCollection;
	}
}
