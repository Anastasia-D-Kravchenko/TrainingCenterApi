# TrainingCenterApi

ASP.NET Core (.NET 10) Web API for a training center: **rooms** and **room reservations**, with in-memory sample data (no database needed).

## Model

* `Room`: name, building code, floor, capacity, projector, active flag
* `Reservation`: room, organizer, topic, date, start/end time, status

## Endpoints

| Method | Path | Description |
|---|---|---|
| GET | `/api/rooms`, `/api/rooms/{id}` | List rooms / get one |
| GET | `/api/rooms/building/{buildingCode}` | Rooms in one building |
| POST / PUT / DELETE | `/api/rooms`, `/api/rooms/{id}` | Create / update / delete a room |
| GET | `/api/reservations`, `/api/reservations/{id}` | List reservations / get one |
| POST / PUT / DELETE | `/api/reservations`, `/api/reservations/{id}` | Create / update / delete a reservation |

Ready-made requests are in [`TrainingCenterApi/TrainingCenterApi.http`](TrainingCenterApi/TrainingCenterApi.http).

## Run

```bash
cd TrainingCenterApi
dotnet run
```

## License

MIT, see [LICENSE](LICENSE).
