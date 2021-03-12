using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000297 RID: 663
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PreferredCultures : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x0007F391 File Offset: 0x0007D591
		public PreferredCultures()
		{
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0007F3A4 File Offset: 0x0007D5A4
		public PreferredCultures(IEnumerable<CultureInfo> cultures)
		{
			if (cultures == null)
			{
				throw new ArgumentNullException();
			}
			this.cultures.AddRange(cultures);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007F3CC File Offset: 0x0007D5CC
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			return this.cultures.GetEnumerator();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0007F3DE File Offset: 0x0007D5DE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0007F3E6 File Offset: 0x0007D5E6
		public int Count
		{
			get
			{
				return this.cultures.Count;
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0007F3F3 File Offset: 0x0007D5F3
		public void Add(CultureInfo newCulture)
		{
			if (newCulture == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.CultureWillFit(newCulture))
			{
				throw new PreferredCulturesException(ServerStrings.TooManyCultures);
			}
			if (!this.cultures.Contains(newCulture))
			{
				this.cultures.Add(newCulture);
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0007F42C File Offset: 0x0007D62C
		public void Remove(CultureInfo info)
		{
			if (info == null)
			{
				throw new ArgumentNullException();
			}
			this.cultures.Remove(info);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0007F444 File Offset: 0x0007D644
		public void Clear()
		{
			this.cultures.Clear();
		}

		// Token: 0x1700088D RID: 2189
		public CultureInfo this[int index]
		{
			get
			{
				return this.cultures[index];
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0007F460 File Offset: 0x0007D660
		public void InsertBefore(CultureInfo cultureAtInsertPoint, CultureInfo cultureToInsert)
		{
			if (!this.CultureWillFit(cultureToInsert))
			{
				throw new PreferredCulturesException(ServerStrings.TooManyCultures);
			}
			if (cultureAtInsertPoint == null || cultureToInsert == null)
			{
				throw new ArgumentNullException();
			}
			if (this.cultures.Contains(cultureToInsert))
			{
				if (LocaleMap.GetLcidFromCulture(cultureToInsert) == LocaleMap.GetLcidFromCulture(cultureAtInsertPoint))
				{
					return;
				}
				this.Remove(cultureToInsert);
			}
			if (!this.cultures.Contains(cultureAtInsertPoint))
			{
				this.Add(cultureToInsert);
				return;
			}
			int index = this.cultures.IndexOf(cultureAtInsertPoint);
			this.cultures.Insert(index, cultureToInsert);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		public void AddSupportedCulture(CultureInfo newCulture, Predicate<CultureInfo> isSupported)
		{
			if (newCulture == null)
			{
				throw new ArgumentNullException("newCulture");
			}
			if (isSupported == null)
			{
				throw new ArgumentNullException("isSupported");
			}
			int num = -1;
			int i = 0;
			while (i < this.cultures.Count)
			{
				CultureInfo cultureInfo = this.cultures[i];
				if (isSupported(cultureInfo))
				{
					if (cultureInfo.Equals(newCulture))
					{
						return;
					}
					num = i;
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.cultures.Contains(newCulture))
			{
				this.cultures.Remove(newCulture);
			}
			while (!this.CultureWillFit(newCulture) && this.cultures.Count > 0)
			{
				this.cultures.RemoveAt(this.cultures.Count - 1);
			}
			if (num < 0)
			{
				this.cultures.Add(newCulture);
				return;
			}
			if (num < this.cultures.Count)
			{
				this.cultures.Insert(num, newCulture);
				return;
			}
			this.cultures.Add(newCulture);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0007F5D0 File Offset: 0x0007D7D0
		private bool CultureWillFit(CultureInfo newCulture)
		{
			if (this.cultures.Contains(newCulture))
			{
				return true;
			}
			ADUser aduser = new ADUser();
			try
			{
				Util.AddRange<CultureInfo, CultureInfo>(aduser.Languages, this.cultures);
				aduser.Languages.Add(newCulture);
			}
			catch (DataValidationException ex)
			{
				if (ex.Error is PropertyConstraintViolationError)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400131F RID: 4895
		private readonly List<CultureInfo> cultures = new List<CultureInfo>();
	}
}
