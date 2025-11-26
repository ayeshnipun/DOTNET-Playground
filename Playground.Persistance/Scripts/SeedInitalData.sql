-- =====================================
-- 1. Insert Categories (50 categories)
-- =====================================
INSERT INTO categories ("Id", "Name")
SELECT gs, 'Category ' || gs
FROM generate_series(1, 50) gs;

-- Reset sequence for categories
SELECT setval(pg_get_serial_sequence('categories', 'Id'), 50);

-- =====================================
-- 2. Insert Users (5000 users)
-- =====================================
INSERT INTO users ("Id", "Username")
SELECT gs, 'user_' || gs
FROM generate_series(1, 5000) gs;

-- Reset sequence for users
SELECT setval(pg_get_serial_sequence('users', 'Id'), 5000);

-- =====================================
-- 3. Insert Posts (7000 posts)
-- =====================================
INSERT INTO posts ("Id", "Content", "UserId", "CategoryId")
SELECT
    gs,
    'This is post ' || gs,
    floor(random() * 5000 + 1)::int,  -- random user_id 1..5000
    floor(random() * 50 + 1)::int     -- random category_id 1..50
FROM generate_series(1, 7000) gs;

-- Reset sequence for posts
SELECT setval(pg_get_serial_sequence('posts', 'Id'), 7000);

-- =====================================
-- 4. Insert Comments (10500 comments)
-- =====================================
INSERT INTO comments ("Id", "Text", "UserId", "PostId", "CreatedAt")
SELECT
    gs,
    'Comment ' || gs,
    floor(random() * 5000 + 1)::int,  -- random user_id
    floor(random() * 7000 + 1)::int,  -- random post_id
    NOW() - (random() * interval '60 days')  -- random timestamp
FROM generate_series(1, 10500) gs;

-- Reset sequence for comments
SELECT setval(pg_get_serial_sequence('comments', 'Id'), 10500);

-- =====================================
-- 5. Insert Likes (12000 likes)
-- =====================================
INSERT INTO likes ("Id", "PostId")
SELECT
    gs,
    floor(random() * 7000 + 1)::int    -- random post_id
FROM generate_series(1, 12000) gs;

-- Reset sequence for likes
SELECT setval(pg_get_serial_sequence('likes', 'Id'), 12000);

