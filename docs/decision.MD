# Decision-Making: City Name vs. Latitude/Longitude

## Overview

When designing the search functionality for bike theft data, a key decision was whether to use a city name (e.g., "Amsterdam") or its corresponding latitude and longitude coordinates. This document outlines the rationale behind the chosen approach, taking into account usability, precision, and implementation complexity.

## Considerations

### Using City Name

**Pros:**
- **Simplicity:** City names are intuitive and easier for users to input and understand, making the search functionality more user-friendly.
- **Readability:** Queries and logs that contain city names are clearer and more easily interpreted by developers and stakeholders.
- **No Geocoding Needed:** Using city names allows for direct queries to be made without needing to convert them into coordinates, streamlining the data retrieval process.

**Cons:**
- **Ambiguity:** Some city names may refer to multiple locations (e.g., Amsterdam in the Netherlands vs. Amsterdam in the U.S.), leading to potential confusion and incorrect results.
- **Geocoding Dependency:** If geocoding is required, it introduces additional complexity and reliance on third-party services, which can impact performance and reliability.

### Using Latitude and Longitude

**Pros:**
- **Precision:** Coordinates uniquely identify specific locations, eliminating ambiguity associated with city names.
- **Spatial Queries:** Latitude and longitude allow for more advanced geographic calculations, such as distance measurements, which can enhance the application's functionality.
- **Reduced Dependency:** Once coordinates are obtained, there is no need for further geocoding, minimizing external service dependencies.

**Cons:**
- **Complexity for Users:** Users may find it challenging to input precise coordinates, which could reduce overall usability and accessibility.
- **Less Readability:** Numeric coordinates can make logs and API requests less interpretable and harder to understand at a glance.

## Conclusion

After weighing the pros and cons, the decision has been made to use **latitude and longitude coordinates** for search functionality. This choice prioritizes precision and the ability to perform complex spatial queries. However, to ensure user-friendliness, the application will accept both city names and coordinates, allowing users to search seamlessly.

By adopting this dual approach, we leverage the strengths of both methods while minimizing their weaknesses, thus enhancing the overall functionality and user experience of the bike theft search feature.
