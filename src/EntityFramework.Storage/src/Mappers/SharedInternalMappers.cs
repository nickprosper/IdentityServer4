using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer4.EntityFramework.Storage.Mappers
{
    internal static class SharedInternalMappers
    {
        internal static string MapAllowedSigningAlgorithms(ICollection<string> sourceMember)
        {
            if (sourceMember == null || !sourceMember.Any())
            {
                return null;
            }
            return sourceMember.Aggregate((x, y) => $"{x},{y}");
        }

        internal static ICollection<string> MapAllowedSigningAlgorithms(string sourceMember)
        {
            var list = new HashSet<string>();
            if (!String.IsNullOrWhiteSpace(sourceMember))
            {
                sourceMember = sourceMember.Trim();
                foreach (var item in sourceMember.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                {
                    list.Add(item);
                }
            }
            return list;
        }

    }
}
