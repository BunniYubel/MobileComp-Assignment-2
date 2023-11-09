#!/bin/bash

git log --pretty='%aN' | sort | uniq -c | sort -rn | while read line ; do
  echo "$line commits";
  AUTHOR=$(echo $line | awk '{print $2}');
  git log --author="$AUTHOR" --pretty=tformat: --numstat |
  awk '{ add += $1; subs += $2; loc += $1 - $2 } END { printf "%s total lines: %s\n", "$AUTHOR", loc }';
done

# # Show number of commits, author name
# git shortlog -s -n

# # Show number of lines, author name
# git ls-files | while read f; do git blame -w -M -C -C --line-porcelain "$f" | grep -I '^author '; done | sort -f | uniq -ic | sort -n --reverse

# Show commit history detail
git log --format="author: %ae" --numstat > unity_client_log.txt


# -------------------------------------------------------------------------------

# # Temporary files to store intermediate results
# TMP_COMMITS=$(mktemp)
# TMP_LINES=$(mktemp)

# # Get number of commits per author
# git shortlog -s -n --all --no-merges | awk '{print $1}' > "${TMP_COMMITS}"

# # Get number of lines per author
# git ls-files | while read file; do git blame --line-porcelain "$file" | grep  "^author "; done | sort | uniq -c | sort -rn > "${TMP_LINES}"

# # Combine the results
# printf "%-20s %-10s %-10s\n" "Author" "Commits" "Lines"
# echo "-------------------------------------------------"
# while IFS= read -r line; do
#     # Extract the number of lines and the author name
#     num_lines=$(echo "$line" | awk '{print $1}')
#     author=$(echo "$line" | cut -d' ' -f2-)

#     # Find the number of commits for the author
#     num_commits=$(grep -m 1 "$author" "${TMP_COMMITS}" | awk '{print $1}' || echo "0")

#     # Output the combined result
#     printf "%-20s %-10s %-10s\n" "$author" "$num_commits" "$num_lines"
# done < "${TMP_LINES}"

# # Remove temporary files
# rm "${TMP_COMMITS}"
# rm "${TMP_LINES}"


# -------------------------------------------------------------------------------


# Show user stats (commits, files modified, insertions, deletions, and total
# lines modified) for a repo

# git_log_opts=( "$@" )

# git log "${git_log_opts[@]}" --format='author: %ae' --numstat \
#     | tr '[A-Z]' '[a-z]' \
#     | grep -v '^$' \
#     | grep -v '^-' \
#     | awk '
#         {
#             if ($1 == "author:") {
#                 author = $2;
#                 commits[author]++;
#             } else {
#                 insertions[author] += $1;
#                 deletions[author] += $2;
#                 total[author] += $1 + $2;
#                 # if this is the first time seeing this file for this
#                 # author, increment their file count
#                 author_file = author ":" $3;
#                 if (!(author_file in seen)) {
#                     seen[author_file] = 1;
#                     files[author]++;
#                 }
#             }
#         }
#         END {
#             # Print a header
#             printf("%-30s\t%-10s\t%-10s\t%-10s\t%-10s\t%-10s\n",
#                    "Email", "Commits", "Files",
#                    "Insertions", "Deletions", "Total Lines");
#             printf("%-30s\t%-10s\t%-10s\t%-10s\t%-10s\t%-10s\n",
#                    "-----", "-------", "-----",
#                    "----------", "---------", "-----------");
            
#             # Print the stats for each user, sorted by total lines
#             n = asorti(total, sorted_emails, "@val_num_desc");
#             for (i = 1; i <= n; i++) {
#                 email = sorted_emails[i];
#                 printf("%-30s\t%-10s\t%-10s\t%-10s\t%-10s\t%-10s\n",
#                        email, commits[email], files[email],
#                        insertions[email], deletions[email], total[email]);
#             }
#         }
# '
