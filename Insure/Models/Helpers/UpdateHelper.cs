using System.Collections.Generic;
using System.Linq;
using Insure.Models.CRM;
using Insure.Models.CRM.News;
using Microsoft.EntityFrameworkCore;

namespace Insure.Models.Helpers
{
    public class UpdateHelper
    {
        public static void UpdateOrCreateRelatedObj(IEnumerable<object> entities, CrmContext db)
        {
            foreach (var entity in entities.Cast<IEntity>())
            {
                if (entity.Id != 0)
                {
                    switch (entity)
                    {
                        case Author author:
                            db.Authors.Attach(author);
                            break;
                        case Tag tag:
                            db.Tags.Attach(tag);
                            break;
                    }

                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Entry(entity).State = EntityState.Added;
                }
            }
        }
    }
}