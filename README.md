# cache-service
This Redis Cache Service is a customizable caching solution designed for .NET Core applications, leveraging Redis as a distributed caching layer. It ensures efficient and consistent caching of data, particularly useful for applications with high traffic and heavy data-fetch operations, such as dashboards and reporting services.
Key Features:

Centralized Caching Service: A dedicated caching service that handles all Redis operations, ensuring consistency and reducing redundancy across multiple services. It enforces standardized naming conventions for cache keys, using enums to improve readability and maintainability.
Background Cache Refresh: Includes a background service that automatically refreshes cache data at configurable intervals. This eliminates the need for the cache to be updated only when a request is made, ensuring the cache remains fresh and up-to-date without relying on user traffic.
Configurable Cache Expiry: Supports flexible cache expiration times based on data freshness requirements, ideal for scenarios like weekly data refresh cycles for dashboards or reporting systems.
Type-Safe Enums for Cache Categories: Utilizes enums to define cache categories, ensuring a type-safe, consistent approach to cache key management, avoiding hard-coded strings and reducing the chance of key collisions.
Scalable & High Performance: By integrating with Redis, the caching service provides low-latency access to frequently requested data, significantly improving the performance of data-intensive applications.
Use Cases:

Dashboard and Reporting Systems: Ideal for applications that require frequent data access with minimal database queries.
Heavy-Read Workloads: Optimizes performance by serving cached data without hitting the underlying data source on every request.
Proactive Cache Updates: Ensures cache data remains fresh and consistent even when no requests are made, thanks to the background auto-refresh feature.
How It Works:

Caching Service: Provides methods for getting, setting, and removing cached data, with enforced naming conventions through type-safe enums.
Background Service: Periodically refreshes cached data by fetching updates from the underlying data source, ensuring that cache remains fresh based on a configured time interval.
Distributed Cache: Redis is used as the distributed cache store, enabling high availability and scalability for large-scale applications.
This Redis Cache Service is designed to be modular, scalable, and easily configurable, making it a great fit for applications requiring efficient data caching and proactive cache management.
