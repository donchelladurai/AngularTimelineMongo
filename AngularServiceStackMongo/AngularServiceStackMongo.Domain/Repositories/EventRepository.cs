﻿namespace AngularServiceStackMongo.Domain
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver.Builders;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class EventRepository : EntityRepository<Event>
    {
        public IEnumerable<Event> GetByTimelineId(string id, int limit, int skip)
        {
            var entityQuery = Query<Event>.EQ(e => e.TimelineId, id);

            var result = this.MongoConnectionHandler.MongoCollection.Find(entityQuery)
                        .SetSortOrder(SortBy<Event>.Ascending(p => p.Name))
                        .SetLimit(limit)
                        .SetSkip(skip);

            return result;
        }
    }
}
